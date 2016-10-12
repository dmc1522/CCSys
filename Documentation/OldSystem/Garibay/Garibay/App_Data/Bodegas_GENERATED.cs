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
public partial class Bodegas
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[Bodegas]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _bodegaID;
	private System.String _bodega;
	
	#endregion
	
	
	#region Properties
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
	
	public System.String Bodega
	{
		get
		{
			return _bodega;
		}
		set
		{
			_bodega = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("bodegaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("bodega", typeof(System.String) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (BodegaID == null)
		dr["bodegaID"] = DBNull.Value;
		else
		dr["bodegaID"] = BodegaID;
		
		if (Bodega == null)
		dr["bodega"] = DBNull.Value;
		else
		dr["bodega"] = Bodega;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		BodegaID = dr["bodegaID"] != DBNull.Value ? Convert.ToInt32(dr["bodegaID"]) : BodegaID = null;
		Bodega = dr["bodega"] != DBNull.Value ? Convert.ToString(dr["bodega"]) : Bodega = null;
	}
	
	public static Bodegas[] MapFrom(DataSet ds)
	{
		List<Bodegas> objects;
		
		
		// Initialise Collection.
		objects = new List<Bodegas>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[Bodegas] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[Bodegas] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			Bodegas instance = new Bodegas();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Bodegas Get(System.Int32 bodegaID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		Bodegas instance;
		
		
		instance = new Bodegas();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBodegas_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, bodegaID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get Bodegas ID:" + bodegaID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? bodegaID, System.String bodega, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBodegas_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, bodegaID, bodega);
		
		if (transaction == null)
		db.ExecuteScalar(dbCommand);
		else
		db.ExecuteScalar(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? bodegaID, System.String bodega)
	{
		Insert(bodegaID, bodega, null);
	}
	/// <summary>
	/// Insert current Bodegas to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(BodegaID, Bodega, transaction);
	}
	
	/// <summary>
	/// Insert current Bodegas to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? bodegaID, System.String bodega, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBodegas_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@bodegaID"].Value = bodegaID;
		dbCommand.Parameters["@bodega"].Value = bodega;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? bodegaID, System.String bodega)
	{
		Update(bodegaID, bodega, null);
	}
	
	public static void Update(Bodegas bodegas)
	{
		bodegas.Update();
	}
	
	public static void Update(Bodegas bodegas, DbTransaction transaction)
	{
		bodegas.Update(transaction);
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
		sqlCommand = "[dbo].gspBodegas_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@bodegaID"].SourceColumn = "bodegaID";
		dbCommand.Parameters["@bodega"].SourceColumn = "bodega";
		
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
	public static void Delete(System.Int32? bodegaID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBodegas_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, bodegaID);
		
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
	public static void Delete(System.Int32? bodegaID)
	{
		Delete(
		bodegaID);
	}
	
	/// <summary>
	/// Delete current Bodegas from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBodegas_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, BodegaID);
		
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
	
	/// <summary>
	/// Delete current Bodegas from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Bodegas[] Search(System.Int32? bodegaID, System.String bodega)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBodegas_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, bodegaID, bodega);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return Bodegas.MapFrom(ds);
	}
	
	
	public static Bodegas[] Search(Bodegas searchObject)
	{
		return Search ( searchObject.BodegaID, searchObject.Bodega);
	}
	
	/// <summary>
	/// Returns all Bodegas objects.
	/// </summary>
	/// <returns>List of all Bodegas objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static Bodegas[] Search()
	{
		return Search ( null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

