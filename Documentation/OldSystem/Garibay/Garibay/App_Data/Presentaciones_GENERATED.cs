/****************************************************************************/
/* Code Author (written by Xin Zhao)                                        */
/*                                                                          */
/* This file was automatically generated using Code Author.                 */
/* Any manual changes to this file will be overwritten by a automated tool. */
/*                                                                          */
/* Date Generated: 20/05/2011 12:35:04 a.m.                                    */
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
public partial class Presentaciones
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[Presentaciones]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _presentacionID;
	private System.String _presentacion;
	private System.Double? _peso;
	
	#endregion
	
	
	#region Properties
	public System.Int32? PresentacionID
	{
		get
		{
			return _presentacionID;
		}
		set
		{
			_presentacionID = value;
		}
	}
	
	public System.String Presentacion
	{
		get
		{
			return _presentacion;
		}
		set
		{
			_presentacion = value;
		}
	}
	
	public System.Double? Peso
	{
		get
		{
			return _peso;
		}
		set
		{
			_peso = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("presentacionID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("Presentacion", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("peso", typeof(System.Double) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (PresentacionID == null)
		dr["presentacionID"] = DBNull.Value;
		else
		dr["presentacionID"] = PresentacionID;
		
		if (Presentacion == null)
		dr["Presentacion"] = DBNull.Value;
		else
		dr["Presentacion"] = Presentacion;
		
		if (Peso == null)
		dr["peso"] = DBNull.Value;
		else
		dr["peso"] = Peso;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		PresentacionID = dr["presentacionID"] != DBNull.Value ? Convert.ToInt32(dr["presentacionID"]) : PresentacionID = null;
		Presentacion = dr["Presentacion"] != DBNull.Value ? Convert.ToString(dr["Presentacion"]) : Presentacion = null;
		Peso = dr["peso"] != DBNull.Value ? Convert.ToDouble(dr["peso"]) : Peso = null;
	}
	
	public static Presentaciones[] MapFrom(DataSet ds)
	{
		List<Presentaciones> objects;
		
		
		// Initialise Collection.
		objects = new List<Presentaciones>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[Presentaciones] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[Presentaciones] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			Presentaciones instance = new Presentaciones();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Presentaciones Get(System.Int32 presentacionID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		Presentaciones instance;
		
		
		instance = new Presentaciones();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspPresentaciones_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, presentacionID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get Presentaciones ID:" + presentacionID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.String presentacion, System.Double? peso, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspPresentaciones_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, presentacion, peso);
		
		if (transaction == null)
		this.PresentacionID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.PresentacionID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.String presentacion, System.Double? peso)
	{
		Insert(presentacion, peso, null);
	}
	/// <summary>
	/// Insert current Presentaciones to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(Presentacion, Peso, transaction);
	}
	
	/// <summary>
	/// Insert current Presentaciones to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? presentacionID, System.String presentacion, System.Double? peso, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspPresentaciones_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@presentacionID"].Value = presentacionID;
		dbCommand.Parameters["@presentacion"].Value = presentacion;
		dbCommand.Parameters["@peso"].Value = peso;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? presentacionID, System.String presentacion, System.Double? peso)
	{
		Update(presentacionID, presentacion, peso, null);
	}
	
	public static void Update(Presentaciones presentaciones)
	{
		presentaciones.Update();
	}
	
	public static void Update(Presentaciones presentaciones, DbTransaction transaction)
	{
		presentaciones.Update(transaction);
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
		sqlCommand = "[dbo].gspPresentaciones_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@presentacionID"].SourceColumn = "presentacionID";
		dbCommand.Parameters["@presentacion"].SourceColumn = "Presentacion";
		dbCommand.Parameters["@peso"].SourceColumn = "peso";
		
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
	public static void Delete(System.Int32? presentacionID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspPresentaciones_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, presentacionID);
		
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
	public static void Delete(System.Int32? presentacionID)
	{
		Delete(
		presentacionID, null);
	}
	
	/// <summary>
	/// Delete current Presentaciones from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspPresentaciones_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, PresentacionID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.PresentacionID = null;
	}
	
	/// <summary>
	/// Delete current Presentaciones from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Presentaciones[] Search(System.Int32? presentacionID, System.String presentacion, System.Double? peso)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspPresentaciones_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, presentacionID, presentacion, peso);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return Presentaciones.MapFrom(ds);
	}
	
	
	public static Presentaciones[] Search(Presentaciones searchObject)
	{
		return Search ( searchObject.PresentacionID, searchObject.Presentacion, searchObject.Peso);
	}
	
	/// <summary>
	/// Returns all Presentaciones objects.
	/// </summary>
	/// <returns>List of all Presentaciones objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static Presentaciones[] Search()
	{
		return Search ( null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

