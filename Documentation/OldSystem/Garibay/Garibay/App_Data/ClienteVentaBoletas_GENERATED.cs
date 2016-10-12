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
public partial class ClienteVentaBoletas
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[ClienteVenta_Boletas]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _clienteBoletaID;
	private System.Int32? _clienteventaID;
	private System.Int32? _boletaID;
	private System.DateTime? _lastEditDate;
	private System.DateTime? _creationDate;
	
	#endregion
	
	
	#region Properties
	public System.Int32? ClienteBoletaID
	{
		get
		{
			return _clienteBoletaID;
		}
		set
		{
			_clienteBoletaID = value;
		}
	}
	
	public System.Int32? ClienteventaID
	{
		get
		{
			return _clienteventaID;
		}
		set
		{
			_clienteventaID = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("clienteBoletaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("clienteventaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("BoletaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("LastEditDate", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("CreationDate", typeof(System.DateTime) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (ClienteBoletaID == null)
		dr["clienteBoletaID"] = DBNull.Value;
		else
		dr["clienteBoletaID"] = ClienteBoletaID;
		
		if (ClienteventaID == null)
		dr["clienteventaID"] = DBNull.Value;
		else
		dr["clienteventaID"] = ClienteventaID;
		
		if (BoletaID == null)
		dr["BoletaID"] = DBNull.Value;
		else
		dr["BoletaID"] = BoletaID;
		
		if (LastEditDate == null)
		dr["LastEditDate"] = DBNull.Value;
		else
		dr["LastEditDate"] = LastEditDate;
		
		if (CreationDate == null)
		dr["CreationDate"] = DBNull.Value;
		else
		dr["CreationDate"] = CreationDate;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		ClienteBoletaID = dr["clienteBoletaID"] != DBNull.Value ? Convert.ToInt32(dr["clienteBoletaID"]) : ClienteBoletaID = null;
		ClienteventaID = dr["clienteventaID"] != DBNull.Value ? Convert.ToInt32(dr["clienteventaID"]) : ClienteventaID = null;
		BoletaID = dr["BoletaID"] != DBNull.Value ? Convert.ToInt32(dr["BoletaID"]) : BoletaID = null;
		LastEditDate = dr["LastEditDate"] != DBNull.Value ? Convert.ToDateTime(dr["LastEditDate"]) : LastEditDate = null;
		CreationDate = dr["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreationDate"]) : CreationDate = null;
	}
	
	public static ClienteVentaBoletas[] MapFrom(DataSet ds)
	{
		List<ClienteVentaBoletas> objects;
		
		
		// Initialise Collection.
		objects = new List<ClienteVentaBoletas>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[ClienteVenta_Boletas] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[ClienteVenta_Boletas] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			ClienteVentaBoletas instance = new ClienteVentaBoletas();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static ClienteVentaBoletas Get(System.Int32 clienteBoletaID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		ClienteVentaBoletas instance;
		
		
		instance = new ClienteVentaBoletas();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClienteVentaBoletas_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, clienteBoletaID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get ClienteVentaBoletas ID:" + clienteBoletaID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? clienteventaID, System.Int32? boletaID, System.DateTime? lastEditDate, System.DateTime? creationDate, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClienteVentaBoletas_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, clienteventaID, boletaID, lastEditDate, creationDate);
		
		if (transaction == null)
		this.ClienteBoletaID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.ClienteBoletaID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? clienteventaID, System.Int32? boletaID, System.DateTime? lastEditDate, System.DateTime? creationDate)
	{
		Insert(clienteventaID, boletaID, lastEditDate, creationDate, null);
	}
	/// <summary>
	/// Insert current ClienteVentaBoletas to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(ClienteventaID, BoletaID, LastEditDate, CreationDate, transaction);
	}
	
	/// <summary>
	/// Insert current ClienteVentaBoletas to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? clienteBoletaID, System.Int32? clienteventaID, System.Int32? boletaID, System.DateTime? lastEditDate, System.DateTime? creationDate, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClienteVentaBoletas_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@clienteBoletaID"].Value = clienteBoletaID;
		dbCommand.Parameters["@clienteventaID"].Value = clienteventaID;
		dbCommand.Parameters["@boletaID"].Value = boletaID;
		dbCommand.Parameters["@lastEditDate"].Value = lastEditDate;
		dbCommand.Parameters["@creationDate"].Value = creationDate;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? clienteBoletaID, System.Int32? clienteventaID, System.Int32? boletaID, System.DateTime? lastEditDate, System.DateTime? creationDate)
	{
		Update(clienteBoletaID, clienteventaID, boletaID, lastEditDate, creationDate, null);
	}
	
	public static void Update(ClienteVentaBoletas clienteVentaBoletas)
	{
		clienteVentaBoletas.Update();
	}
	
	public static void Update(ClienteVentaBoletas clienteVentaBoletas, DbTransaction transaction)
	{
		clienteVentaBoletas.Update(transaction);
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
		sqlCommand = "[dbo].gspClienteVentaBoletas_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@clienteBoletaID"].SourceColumn = "clienteBoletaID";
		dbCommand.Parameters["@clienteventaID"].SourceColumn = "clienteventaID";
		dbCommand.Parameters["@boletaID"].SourceColumn = "BoletaID";
		dbCommand.Parameters["@lastEditDate"].SourceColumn = "LastEditDate";
		dbCommand.Parameters["@creationDate"].SourceColumn = "CreationDate";
		
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
	public static void Delete(System.Int32? clienteBoletaID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClienteVentaBoletas_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, clienteBoletaID);
		
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
	public static void Delete(System.Int32? clienteBoletaID)
	{
		Delete(
		clienteBoletaID);
	}
	
	/// <summary>
	/// Delete current ClienteVentaBoletas from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClienteVentaBoletas_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, ClienteBoletaID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.ClienteBoletaID = null;
	}
	
	/// <summary>
	/// Delete current ClienteVentaBoletas from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static ClienteVentaBoletas[] Search(System.Int32? clienteBoletaID, System.Int32? clienteventaID, System.Int32? boletaID, System.DateTime? lastEditDate, System.DateTime? creationDate)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClienteVentaBoletas_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, clienteBoletaID, clienteventaID, boletaID, lastEditDate, creationDate);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return ClienteVentaBoletas.MapFrom(ds);
	}
	
	
	public static ClienteVentaBoletas[] Search(ClienteVentaBoletas searchObject)
	{
		return Search ( searchObject.ClienteBoletaID, searchObject.ClienteventaID, searchObject.BoletaID, searchObject.LastEditDate, searchObject.CreationDate);
	}
	
	/// <summary>
	/// Returns all ClienteVentaBoletas objects.
	/// </summary>
	/// <returns>List of all ClienteVentaBoletas objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static ClienteVentaBoletas[] Search()
	{
		return Search ( null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

