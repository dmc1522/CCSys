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
public partial class Transportistas
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[Transportistas]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _transportistaID;
	private System.String _apaterno;
	private System.String _amaterno;
	private System.String _nombres;
	private System.String _domicilio;
	private System.String _poblacion;
	
	#endregion
	
	
	#region Properties
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
	
	public System.String Apaterno
	{
		get
		{
			return _apaterno;
		}
		set
		{
			_apaterno = value;
		}
	}
	
	public System.String Amaterno
	{
		get
		{
			return _amaterno;
		}
		set
		{
			_amaterno = value;
		}
	}
	
	public System.String Nombres
	{
		get
		{
			return _nombres;
		}
		set
		{
			_nombres = value;
		}
	}
	
	public System.String Domicilio
	{
		get
		{
			return _domicilio;
		}
		set
		{
			_domicilio = value;
		}
	}
	
	public System.String Poblacion
	{
		get
		{
			return _poblacion;
		}
		set
		{
			_poblacion = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("transportistaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("apaterno", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("amaterno", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("nombres", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("Domicilio", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("Poblacion", typeof(System.String) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (TransportistaID == null)
		dr["transportistaID"] = DBNull.Value;
		else
		dr["transportistaID"] = TransportistaID;
		
		if (Apaterno == null)
		dr["apaterno"] = DBNull.Value;
		else
		dr["apaterno"] = Apaterno;
		
		if (Amaterno == null)
		dr["amaterno"] = DBNull.Value;
		else
		dr["amaterno"] = Amaterno;
		
		if (Nombres == null)
		dr["nombres"] = DBNull.Value;
		else
		dr["nombres"] = Nombres;
		
		if (Domicilio == null)
		dr["Domicilio"] = DBNull.Value;
		else
		dr["Domicilio"] = Domicilio;
		
		if (Poblacion == null)
		dr["Poblacion"] = DBNull.Value;
		else
		dr["Poblacion"] = Poblacion;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		TransportistaID = dr["transportistaID"] != DBNull.Value ? Convert.ToInt32(dr["transportistaID"]) : TransportistaID = null;
		Apaterno = dr["apaterno"] != DBNull.Value ? Convert.ToString(dr["apaterno"]) : Apaterno = null;
		Amaterno = dr["amaterno"] != DBNull.Value ? Convert.ToString(dr["amaterno"]) : Amaterno = null;
		Nombres = dr["nombres"] != DBNull.Value ? Convert.ToString(dr["nombres"]) : Nombres = null;
		Domicilio = dr["Domicilio"] != DBNull.Value ? Convert.ToString(dr["Domicilio"]) : Domicilio = null;
		Poblacion = dr["Poblacion"] != DBNull.Value ? Convert.ToString(dr["Poblacion"]) : Poblacion = null;
	}
	
	public static Transportistas[] MapFrom(DataSet ds)
	{
		List<Transportistas> objects;
		
		
		// Initialise Collection.
		objects = new List<Transportistas>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[Transportistas] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[Transportistas] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			Transportistas instance = new Transportistas();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Transportistas Get(System.Int32 transportistaID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		Transportistas instance;
		
		
		instance = new Transportistas();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspTransportistas_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, transportistaID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get Transportistas ID:" + transportistaID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.String apaterno, System.String amaterno, System.String nombres, System.String domicilio, System.String poblacion, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspTransportistas_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, apaterno, amaterno, nombres, domicilio, poblacion);
		
		if (transaction == null)
		this.TransportistaID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.TransportistaID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.String apaterno, System.String amaterno, System.String nombres, System.String domicilio, System.String poblacion)
	{
		Insert(apaterno, amaterno, nombres, domicilio, poblacion, null);
	}
	/// <summary>
	/// Insert current Transportistas to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(Apaterno, Amaterno, Nombres, Domicilio, Poblacion, transaction);
	}
	
	/// <summary>
	/// Insert current Transportistas to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? transportistaID, System.String apaterno, System.String amaterno, System.String nombres, System.String domicilio, System.String poblacion, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspTransportistas_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@transportistaID"].Value = transportistaID;
		dbCommand.Parameters["@apaterno"].Value = apaterno;
		dbCommand.Parameters["@amaterno"].Value = amaterno;
		dbCommand.Parameters["@nombres"].Value = nombres;
		dbCommand.Parameters["@domicilio"].Value = domicilio;
		dbCommand.Parameters["@poblacion"].Value = poblacion;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? transportistaID, System.String apaterno, System.String amaterno, System.String nombres, System.String domicilio, System.String poblacion)
	{
		Update(transportistaID, apaterno, amaterno, nombres, domicilio, poblacion, null);
	}
	
	public static void Update(Transportistas transportistas)
	{
		transportistas.Update();
	}
	
	public static void Update(Transportistas transportistas, DbTransaction transaction)
	{
		transportistas.Update(transaction);
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
		sqlCommand = "[dbo].gspTransportistas_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@transportistaID"].SourceColumn = "transportistaID";
		dbCommand.Parameters["@apaterno"].SourceColumn = "apaterno";
		dbCommand.Parameters["@amaterno"].SourceColumn = "amaterno";
		dbCommand.Parameters["@nombres"].SourceColumn = "nombres";
		dbCommand.Parameters["@domicilio"].SourceColumn = "Domicilio";
		dbCommand.Parameters["@poblacion"].SourceColumn = "Poblacion";
		
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
	public static void Delete(System.Int32? transportistaID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspTransportistas_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, transportistaID);
		
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
	public static void Delete(System.Int32? transportistaID)
	{
		Delete(
		transportistaID);
	}
	
	/// <summary>
	/// Delete current Transportistas from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspTransportistas_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, TransportistaID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.TransportistaID = null;
	}
	
	/// <summary>
	/// Delete current Transportistas from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Transportistas[] Search(System.Int32? transportistaID, System.String apaterno, System.String amaterno, System.String nombres, System.String domicilio, System.String poblacion)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspTransportistas_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, transportistaID, apaterno, amaterno, nombres, domicilio, poblacion);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return Transportistas.MapFrom(ds);
	}
	
	
	public static Transportistas[] Search(Transportistas searchObject)
	{
		return Search ( searchObject.TransportistaID, searchObject.Apaterno, searchObject.Amaterno, searchObject.Nombres, searchObject.Domicilio, searchObject.Poblacion);
	}
	
	/// <summary>
	/// Returns all Transportistas objects.
	/// </summary>
	/// <returns>List of all Transportistas objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static Transportistas[] Search()
	{
		return Search ( null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

