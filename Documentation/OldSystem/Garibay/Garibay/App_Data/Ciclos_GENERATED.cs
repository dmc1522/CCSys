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
public partial class Ciclos
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[Ciclos]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _cicloID;
	private System.String _cicloName;
	private System.DateTime? _fechaInicio;
	private System.DateTime? _fechaFinZona1;
	private System.DateTime? _fechaFinZona2;
	private System.Double? _montoporhectarea;
	private System.Boolean? _cerrado;
	
	#endregion
	
	
	#region Properties
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
	
	public System.String CicloName
	{
		get
		{
			return _cicloName;
		}
		set
		{
			_cicloName = value;
		}
	}
	
	public System.DateTime? FechaInicio
	{
		get
		{
			return _fechaInicio;
		}
		set
		{
			_fechaInicio = value;
		}
	}
	
	public System.DateTime? FechaFinZona1
	{
		get
		{
			return _fechaFinZona1;
		}
		set
		{
			_fechaFinZona1 = value;
		}
	}
	
	public System.DateTime? FechaFinZona2
	{
		get
		{
			return _fechaFinZona2;
		}
		set
		{
			_fechaFinZona2 = value;
		}
	}
	
	public System.Double? Montoporhectarea
	{
		get
		{
			return _montoporhectarea;
		}
		set
		{
			_montoporhectarea = value;
		}
	}
	
	public System.Boolean? Cerrado
	{
		get
		{
			return _cerrado;
		}
		set
		{
			_cerrado = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("cicloID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("CicloName", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("fechaInicio", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("fechaFinZona1", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("fechaFinZona2", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("Montoporhectarea", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("cerrado", typeof(System.Boolean) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (CicloID == null)
		dr["cicloID"] = DBNull.Value;
		else
		dr["cicloID"] = CicloID;
		
		if (CicloName == null)
		dr["CicloName"] = DBNull.Value;
		else
		dr["CicloName"] = CicloName;
		
		if (FechaInicio == null)
		dr["fechaInicio"] = DBNull.Value;
		else
		dr["fechaInicio"] = FechaInicio;
		
		if (FechaFinZona1 == null)
		dr["fechaFinZona1"] = DBNull.Value;
		else
		dr["fechaFinZona1"] = FechaFinZona1;
		
		if (FechaFinZona2 == null)
		dr["fechaFinZona2"] = DBNull.Value;
		else
		dr["fechaFinZona2"] = FechaFinZona2;
		
		if (Montoporhectarea == null)
		dr["Montoporhectarea"] = DBNull.Value;
		else
		dr["Montoporhectarea"] = Montoporhectarea;
		
		if (Cerrado == null)
		dr["cerrado"] = DBNull.Value;
		else
		dr["cerrado"] = Cerrado;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		CicloID = dr["cicloID"] != DBNull.Value ? Convert.ToInt32(dr["cicloID"]) : CicloID = null;
		CicloName = dr["CicloName"] != DBNull.Value ? Convert.ToString(dr["CicloName"]) : CicloName = null;
		FechaInicio = dr["fechaInicio"] != DBNull.Value ? Convert.ToDateTime(dr["fechaInicio"]) : FechaInicio = null;
		FechaFinZona1 = dr["fechaFinZona1"] != DBNull.Value ? Convert.ToDateTime(dr["fechaFinZona1"]) : FechaFinZona1 = null;
		FechaFinZona2 = dr["fechaFinZona2"] != DBNull.Value ? Convert.ToDateTime(dr["fechaFinZona2"]) : FechaFinZona2 = null;
		Montoporhectarea = dr["Montoporhectarea"] != DBNull.Value ? Convert.ToDouble(dr["Montoporhectarea"]) : Montoporhectarea = null;
		Cerrado = dr["cerrado"] != DBNull.Value ? Convert.ToBoolean(dr["cerrado"]) : Cerrado = null;
	}
	
	public static Ciclos[] MapFrom(DataSet ds)
	{
		List<Ciclos> objects;
		
		
		// Initialise Collection.
		objects = new List<Ciclos>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[Ciclos] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[Ciclos] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			Ciclos instance = new Ciclos();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Ciclos Get(System.Int32 cicloID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		Ciclos instance;
		
		
		instance = new Ciclos();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspCiclos_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, cicloID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get Ciclos ID:" + cicloID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.String cicloName, System.DateTime? fechaInicio, System.DateTime? fechaFinZona1, System.DateTime? fechaFinZona2, System.Double? montoporhectarea, System.Boolean? cerrado, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspCiclos_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, cicloName, fechaInicio, fechaFinZona1, fechaFinZona2, montoporhectarea, cerrado);
		
		if (transaction == null)
		this.CicloID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.CicloID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.String cicloName, System.DateTime? fechaInicio, System.DateTime? fechaFinZona1, System.DateTime? fechaFinZona2, System.Double? montoporhectarea, System.Boolean? cerrado)
	{
		Insert(cicloName, fechaInicio, fechaFinZona1, fechaFinZona2, montoporhectarea, cerrado, null);
	}
	/// <summary>
	/// Insert current Ciclos to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(CicloName, FechaInicio, FechaFinZona1, FechaFinZona2, Montoporhectarea, Cerrado, transaction);
	}
	
	/// <summary>
	/// Insert current Ciclos to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? cicloID, System.String cicloName, System.DateTime? fechaInicio, System.DateTime? fechaFinZona1, System.DateTime? fechaFinZona2, System.Double? montoporhectarea, System.Boolean? cerrado, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspCiclos_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@cicloID"].Value = cicloID;
		dbCommand.Parameters["@cicloName"].Value = cicloName;
		dbCommand.Parameters["@fechaInicio"].Value = fechaInicio;
		dbCommand.Parameters["@fechaFinZona1"].Value = fechaFinZona1;
		dbCommand.Parameters["@fechaFinZona2"].Value = fechaFinZona2;
		dbCommand.Parameters["@montoporhectarea"].Value = montoporhectarea;
		dbCommand.Parameters["@cerrado"].Value = cerrado;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? cicloID, System.String cicloName, System.DateTime? fechaInicio, System.DateTime? fechaFinZona1, System.DateTime? fechaFinZona2, System.Double? montoporhectarea, System.Boolean? cerrado)
	{
		Update(cicloID, cicloName, fechaInicio, fechaFinZona1, fechaFinZona2, montoporhectarea, cerrado, null);
	}
	
	public static void Update(Ciclos ciclos)
	{
		ciclos.Update();
	}
	
	public static void Update(Ciclos ciclos, DbTransaction transaction)
	{
		ciclos.Update(transaction);
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
		sqlCommand = "[dbo].gspCiclos_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@cicloID"].SourceColumn = "cicloID";
		dbCommand.Parameters["@cicloName"].SourceColumn = "CicloName";
		dbCommand.Parameters["@fechaInicio"].SourceColumn = "fechaInicio";
		dbCommand.Parameters["@fechaFinZona1"].SourceColumn = "fechaFinZona1";
		dbCommand.Parameters["@fechaFinZona2"].SourceColumn = "fechaFinZona2";
		dbCommand.Parameters["@montoporhectarea"].SourceColumn = "Montoporhectarea";
		dbCommand.Parameters["@cerrado"].SourceColumn = "cerrado";
		
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
	public static void Delete(System.Int32? cicloID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspCiclos_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, cicloID);
		
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
	public static void Delete(System.Int32? cicloID)
	{
		Delete(
		cicloID);
	}
	
	/// <summary>
	/// Delete current Ciclos from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspCiclos_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, CicloID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.CicloID = null;
	}
	
	/// <summary>
	/// Delete current Ciclos from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Ciclos[] Search(System.Int32? cicloID, System.String cicloName, System.DateTime? fechaInicio, System.DateTime? fechaFinZona1, System.DateTime? fechaFinZona2, System.Double? montoporhectarea, System.Boolean? cerrado)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspCiclos_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, cicloID, cicloName, fechaInicio, fechaFinZona1, fechaFinZona2, montoporhectarea, cerrado);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return Ciclos.MapFrom(ds);
	}
	
	
	public static Ciclos[] Search(Ciclos searchObject)
	{
		return Search ( searchObject.CicloID, searchObject.CicloName, searchObject.FechaInicio, searchObject.FechaFinZona1, searchObject.FechaFinZona2, searchObject.Montoporhectarea, searchObject.Cerrado);
	}
	
	/// <summary>
	/// Returns all Ciclos objects.
	/// </summary>
	/// <returns>List of all Ciclos objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static Ciclos[] Search()
	{
		return Search ( null, null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

