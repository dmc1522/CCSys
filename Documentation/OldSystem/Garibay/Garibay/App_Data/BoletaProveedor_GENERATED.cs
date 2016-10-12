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
public partial class BoletaProveedor
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[boleta_proveedor]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _bolProvID;
	private System.Int32? _boletaID;
	private System.Int32? _proveedorID;
	
	#endregion
	
	
	#region Properties
	public System.Int32? BolProvID
	{
		get
		{
			return _bolProvID;
		}
		set
		{
			_bolProvID = value;
		}
	}
	
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
		
		ds.Tables[TABLE_NAME].Columns.Add("bolProvID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("boletaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("proveedorID", typeof(System.Int32) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (BolProvID == null)
		dr["bolProvID"] = DBNull.Value;
		else
		dr["bolProvID"] = BolProvID;
		
		if (BoletaID == null)
		dr["boletaID"] = DBNull.Value;
		else
		dr["boletaID"] = BoletaID;
		
		if (ProveedorID == null)
		dr["proveedorID"] = DBNull.Value;
		else
		dr["proveedorID"] = ProveedorID;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		BolProvID = dr["bolProvID"] != DBNull.Value ? Convert.ToInt32(dr["bolProvID"]) : BolProvID = null;
		BoletaID = dr["boletaID"] != DBNull.Value ? Convert.ToInt32(dr["boletaID"]) : BoletaID = null;
		ProveedorID = dr["proveedorID"] != DBNull.Value ? Convert.ToInt32(dr["proveedorID"]) : ProveedorID = null;
	}
	
	public static BoletaProveedor[] MapFrom(DataSet ds)
	{
		List<BoletaProveedor> objects;
		
		
		// Initialise Collection.
		objects = new List<BoletaProveedor>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[boleta_proveedor] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[boleta_proveedor] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			BoletaProveedor instance = new BoletaProveedor();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static BoletaProveedor Get(System.Int32 bolProvID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		BoletaProveedor instance;
		
		
		instance = new BoletaProveedor();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletaProveedor_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, bolProvID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get BoletaProveedor ID:" + bolProvID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? boletaID, System.Int32? proveedorID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletaProveedor_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, boletaID, proveedorID);
		
		if (transaction == null)
		this.BolProvID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.BolProvID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? boletaID, System.Int32? proveedorID)
	{
		Insert(boletaID, proveedorID, null);
	}
	/// <summary>
	/// Insert current BoletaProveedor to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(BoletaID, ProveedorID, transaction);
	}
	
	/// <summary>
	/// Insert current BoletaProveedor to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? bolProvID, System.Int32? boletaID, System.Int32? proveedorID, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletaProveedor_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@bolProvID"].Value = bolProvID;
		dbCommand.Parameters["@boletaID"].Value = boletaID;
		dbCommand.Parameters["@proveedorID"].Value = proveedorID;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? bolProvID, System.Int32? boletaID, System.Int32? proveedorID)
	{
		Update(bolProvID, boletaID, proveedorID, null);
	}
	
	public static void Update(BoletaProveedor boletaProveedor)
	{
		boletaProveedor.Update();
	}
	
	public static void Update(BoletaProveedor boletaProveedor, DbTransaction transaction)
	{
		boletaProveedor.Update(transaction);
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
		sqlCommand = "[dbo].gspBoletaProveedor_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@bolProvID"].SourceColumn = "bolProvID";
		dbCommand.Parameters["@boletaID"].SourceColumn = "boletaID";
		dbCommand.Parameters["@proveedorID"].SourceColumn = "proveedorID";
		
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
	public static void Delete(System.Int32? bolProvID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletaProveedor_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, bolProvID);
		
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
	public static void Delete(System.Int32? bolProvID)
	{
		Delete(
		bolProvID);
	}
	
	/// <summary>
	/// Delete current BoletaProveedor from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletaProveedor_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, BolProvID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.BolProvID = null;
	}
	
	/// <summary>
	/// Delete current BoletaProveedor from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static BoletaProveedor[] Search(System.Int32? bolProvID, System.Int32? boletaID, System.Int32? proveedorID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletaProveedor_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, bolProvID, boletaID, proveedorID);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return BoletaProveedor.MapFrom(ds);
	}
	
	
	public static BoletaProveedor[] Search(BoletaProveedor searchObject)
	{
		return Search ( searchObject.BolProvID, searchObject.BoletaID, searchObject.ProveedorID);
	}
	
	/// <summary>
	/// Returns all BoletaProveedor objects.
	/// </summary>
	/// <returns>List of all BoletaProveedor objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static BoletaProveedor[] Search()
	{
		return Search ( null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

