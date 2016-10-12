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
public partial class NotasDeCompraDetalle
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[NotasDeCompra_Detalle]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _ndCdetalleID;
	private System.Int32? _notadecompraID;
	private System.Int32? _productoID;
	private System.Int32? _bodegaID;
	private System.Double? _cantidad;
	private System.Decimal? _preciodecompra;
	private System.Double? _sacos;
	
	#endregion
	
	
	#region Properties
	public System.Int32? NDCdetalleID
	{
		get
		{
			return _ndCdetalleID;
		}
		set
		{
			_ndCdetalleID = value;
		}
	}
	
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
	
	public System.Decimal? Preciodecompra
	{
		get
		{
			return _preciodecompra;
		}
		set
		{
			_preciodecompra = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("NDCdetalleID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("notadecompraID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("productoID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("bodegaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("cantidad", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("preciodecompra", typeof(System.Decimal) );
		ds.Tables[TABLE_NAME].Columns.Add("sacos", typeof(System.Double) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (NDCdetalleID == null)
		dr["NDCdetalleID"] = DBNull.Value;
		else
		dr["NDCdetalleID"] = NDCdetalleID;
		
		if (NotadecompraID == null)
		dr["notadecompraID"] = DBNull.Value;
		else
		dr["notadecompraID"] = NotadecompraID;
		
		if (ProductoID == null)
		dr["productoID"] = DBNull.Value;
		else
		dr["productoID"] = ProductoID;
		
		if (BodegaID == null)
		dr["bodegaID"] = DBNull.Value;
		else
		dr["bodegaID"] = BodegaID;
		
		if (Cantidad == null)
		dr["cantidad"] = DBNull.Value;
		else
		dr["cantidad"] = Cantidad;
		
		if (Preciodecompra == null)
		dr["preciodecompra"] = DBNull.Value;
		else
		dr["preciodecompra"] = Preciodecompra;
		
		if (Sacos == null)
		dr["sacos"] = DBNull.Value;
		else
		dr["sacos"] = Sacos;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}

    /*
	private void MapFrom(DataRow dr)
		{
			NDCdetalleID = dr["NDCdetalleID"] != DBNull.Value ? Convert.ToInt32(dr["NDCdetalleID"]) : NDCdetalleID = null;
			NotadecompraID = dr["notadecompraID"] != DBNull.Value ? Convert.ToInt32(dr["notadecompraID"]) : NotadecompraID = null;
			ProductoID = dr["productoID"] != DBNull.Value ? Convert.ToInt32(dr["productoID"]) : ProductoID = null;
			BodegaID = dr["bodegaID"] != DBNull.Value ? Convert.ToInt32(dr["bodegaID"]) : BodegaID = null;
			Cantidad = dr["cantidad"] != DBNull.Value ? Convert.ToDouble(dr["cantidad"]) : Cantidad = null;
			Preciodecompra = dr["preciodecompra"] != DBNull.Value ? Convert.ToDecimal(dr["preciodecompra"]) : Preciodecompra = null;
			Sacos = dr["sacos"] != DBNull.Value ? Convert.ToDouble(dr["sacos"]) : Sacos = null;
		}*/
	
	
	public static NotasDeCompraDetalle[] MapFrom(DataSet ds)
	{
		List<NotasDeCompraDetalle> objects;
		
		
		// Initialise Collection.
		objects = new List<NotasDeCompraDetalle>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[NotasDeCompra_Detalle] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[NotasDeCompra_Detalle] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			NotasDeCompraDetalle instance = new NotasDeCompraDetalle();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static NotasDeCompraDetalle Get(System.Int32 ndCdetalleID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		NotasDeCompraDetalle instance;
		
		
		instance = new NotasDeCompraDetalle();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompraDetalle_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, ndCdetalleID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get NotasDeCompraDetalle ID:" + ndCdetalleID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? notadecompraID, System.Int32? productoID, System.Int32? bodegaID, System.Double? cantidad, System.Decimal? preciodecompra, System.Double? sacos, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompraDetalle_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, notadecompraID, productoID, bodegaID, cantidad, preciodecompra, sacos);
		
		if (transaction == null)
		this.NDCdetalleID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.NDCdetalleID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? notadecompraID, System.Int32? productoID, System.Int32? bodegaID, System.Double? cantidad, System.Decimal? preciodecompra, System.Double? sacos)
	{
		Insert(notadecompraID, productoID, bodegaID, cantidad, preciodecompra, sacos, null);
	}
	/// <summary>
	/// Insert current NotasDeCompraDetalle to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(NotadecompraID, ProductoID, BodegaID, Cantidad, Preciodecompra, Sacos, transaction);
	}
	
	/// <summary>
	/// Insert current NotasDeCompraDetalle to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? ndCdetalleID, System.Int32? notadecompraID, System.Int32? productoID, System.Int32? bodegaID, System.Double? cantidad, System.Decimal? preciodecompra, System.Double? sacos, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompraDetalle_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@ndCdetalleID"].Value = ndCdetalleID;
		dbCommand.Parameters["@notadecompraID"].Value = notadecompraID;
		dbCommand.Parameters["@productoID"].Value = productoID;
		dbCommand.Parameters["@bodegaID"].Value = bodegaID;
		dbCommand.Parameters["@cantidad"].Value = cantidad;
		dbCommand.Parameters["@preciodecompra"].Value = preciodecompra;
		dbCommand.Parameters["@sacos"].Value = sacos;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? ndCdetalleID, System.Int32? notadecompraID, System.Int32? productoID, System.Int32? bodegaID, System.Double? cantidad, System.Decimal? preciodecompra, System.Double? sacos)
	{
		Update(ndCdetalleID, notadecompraID, productoID, bodegaID, cantidad, preciodecompra, sacos, null);
	}
	
	public static void Update(NotasDeCompraDetalle notasDeCompraDetalle)
	{
		notasDeCompraDetalle.Update();
	}
	
	public static void Update(NotasDeCompraDetalle notasDeCompraDetalle, DbTransaction transaction)
	{
		notasDeCompraDetalle.Update(transaction);
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
		sqlCommand = "[dbo].gspNotasDeCompraDetalle_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@ndCdetalleID"].SourceColumn = "NDCdetalleID";
		dbCommand.Parameters["@notadecompraID"].SourceColumn = "notadecompraID";
		dbCommand.Parameters["@productoID"].SourceColumn = "productoID";
		dbCommand.Parameters["@bodegaID"].SourceColumn = "bodegaID";
		dbCommand.Parameters["@cantidad"].SourceColumn = "cantidad";
		dbCommand.Parameters["@preciodecompra"].SourceColumn = "preciodecompra";
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
	public static void Delete(System.Int32? ndCdetalleID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompraDetalle_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, ndCdetalleID);
		
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
	public static void Delete(System.Int32? ndCdetalleID)
	{
		Delete(
		ndCdetalleID);
	}
	
	/// <summary>
	/// Delete current NotasDeCompraDetalle from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompraDetalle_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, NDCdetalleID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.NDCdetalleID = null;
	}
	
	/// <summary>
	/// Delete current NotasDeCompraDetalle from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static NotasDeCompraDetalle[] Search(System.Int32? ndCdetalleID, System.Int32? notadecompraID, System.Int32? productoID, System.Int32? bodegaID, System.Double? cantidad, System.Decimal? preciodecompra, System.Double? sacos)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompraDetalle_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, ndCdetalleID, notadecompraID, productoID, bodegaID, cantidad, preciodecompra, sacos);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return NotasDeCompraDetalle.MapFrom(ds);
	}
	
	
	public static NotasDeCompraDetalle[] Search(NotasDeCompraDetalle searchObject)
	{
		return Search ( searchObject.NDCdetalleID, searchObject.NotadecompraID, searchObject.ProductoID, searchObject.BodegaID, searchObject.Cantidad, searchObject.Preciodecompra, searchObject.Sacos);
	}
	
	/// <summary>
	/// Returns all NotasDeCompraDetalle objects.
	/// </summary>
	/// <returns>List of all NotasDeCompraDetalle objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static NotasDeCompraDetalle[] Search()
	{
		return Search ( null, null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

