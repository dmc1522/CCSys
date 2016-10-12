/****************************************************************************/
/* Code Author (written by Xin Zhao)                                        */
/*                                                                          */
/* This file was automatically generated using Code Author.                 */
/* Any manual changes to this file will be overwritten by a automated tool. */
/*                                                                          */
/* Date Generated: 14/05/2011 03:55:20 p.m.                                    */
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
public partial class Estados
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "Estados";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _estadoID;
	private System.String _estado;
	
	#endregion
	
	
	#region Properties
	public System.Int32? EstadoID
	{
		get
		{
			return _estadoID;
		}
		set
		{
			_estadoID = value;
		}
	}
	
	public System.String Estado
	{
		get
		{
			return _estado;
		}
		set
		{
			_estado = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("estadoID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("estado", typeof(System.String) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (EstadoID == null)
		dr["estadoID"] = DBNull.Value;
		else
		dr["estadoID"] = EstadoID;
		
		if (Estado == null)
		dr["estado"] = DBNull.Value;
		else
		dr["estado"] = Estado;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		EstadoID = dr["estadoID"] != DBNull.Value ? Convert.ToInt32(dr["estadoID"]) : EstadoID = null;
		Estado = dr["estado"] != DBNull.Value ? Convert.ToString(dr["estado"]) : Estado = null;
	}
	
	public static Estados[] MapFrom(DataSet ds)
	{
		List<Estados> objects;
		
		
		// Initialise Collection.
		objects = new List<Estados>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[Estados] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[Estados] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			Estados instance = new Estados();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Estados Get(System.Int32 estadoID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		Estados instance;
		
		
		instance = new Estados();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEstados_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, estadoID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get Estados ID:" + estadoID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? estadoID, System.String estado, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEstados_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, estadoID, estado);
		
		if (transaction == null)
		db.ExecuteScalar(dbCommand);
		else
		db.ExecuteScalar(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? estadoID, System.String estado)
	{
		Insert(estadoID, estado, null);
	}
	/// <summary>
	/// Insert current Estados to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(EstadoID, Estado, transaction);
	}
	
	/// <summary>
	/// Insert current Estados to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? estadoID, System.String estado, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEstados_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@estadoID"].Value = estadoID;
		dbCommand.Parameters["@estado"].Value = estado;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? estadoID, System.String estado)
	{
		Update(estadoID, estado, null);
	}
	
	public static void Update(Estados estados)
	{
		estados.Update();
	}
	
	public static void Update(Estados estados, DbTransaction transaction)
	{
		estados.Update(transaction);
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
		sqlCommand = "[dbo].gspEstados_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@estadoID"].SourceColumn = "estadoID";
		dbCommand.Parameters["@estado"].SourceColumn = "estado";
		
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
	public static void Delete(System.Int32? estadoID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEstados_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, estadoID);
		
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
	public static void Delete(System.Int32? estadoID)
	{
		Delete(
		estadoID);
	}
	
	/// <summary>
	/// Delete current Estados from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEstados_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, EstadoID);
		
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
	/// Delete current Estados from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Estados[] Search(System.Int32? estadoID, System.String estado)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEstados_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, estadoID, estado);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return Estados.MapFrom(ds);
	}
	
	
	public static Estados[] Search(Estados searchObject)
	{
		return Search ( searchObject.EstadoID, searchObject.Estado);
	}
	
	/// <summary>
	/// Returns all Estados objects.
	/// </summary>
	/// <returns>List of all Estados objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static Estados[] Search()
	{
		return Search ( null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

