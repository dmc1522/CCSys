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
public partial class SolicitudesSisBancosSeguroAgricola
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[SolicitudesSisBancos_SeguroAgricola]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _solSaID;
	private System.Int32? _solicitudID;
	private System.Int32? _seguroID;
	private System.Double? _hectAseguradas;
	private System.String _descParcelas;
	private System.Double? _costoTotalSeguro;
	
	#endregion
	
	
	#region Properties
	public System.Int32? SolSaID
	{
		get
		{
			return _solSaID;
		}
		set
		{
			_solSaID = value;
		}
	}
	
	public System.Int32? SolicitudID
	{
		get
		{
			return _solicitudID;
		}
		set
		{
			_solicitudID = value;
		}
	}
	
	public System.Int32? SeguroID
	{
		get
		{
			return _seguroID;
		}
		set
		{
			_seguroID = value;
		}
	}
	
	public System.Double? HectAseguradas
	{
		get
		{
			return _hectAseguradas;
		}
		set
		{
			_hectAseguradas = value;
		}
	}
	
	public System.String DescParcelas
	{
		get
		{
			return _descParcelas;
		}
		set
		{
			_descParcelas = value;
		}
	}
	
	public System.Double? CostoTotalSeguro
	{
		get
		{
			return _costoTotalSeguro;
		}
		set
		{
			_costoTotalSeguro = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("sol_sa_ID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("solicitudID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("seguroID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("hectAseguradas", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("descParcelas", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("CostoTotalSeguro", typeof(System.Double) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (SolSaID == null)
		dr["sol_sa_ID"] = DBNull.Value;
		else
		dr["sol_sa_ID"] = SolSaID;
		
		if (SolicitudID == null)
		dr["solicitudID"] = DBNull.Value;
		else
		dr["solicitudID"] = SolicitudID;
		
		if (SeguroID == null)
		dr["seguroID"] = DBNull.Value;
		else
		dr["seguroID"] = SeguroID;
		
		if (HectAseguradas == null)
		dr["hectAseguradas"] = DBNull.Value;
		else
		dr["hectAseguradas"] = HectAseguradas;
		
		if (DescParcelas == null)
		dr["descParcelas"] = DBNull.Value;
		else
		dr["descParcelas"] = DescParcelas;
		
		if (CostoTotalSeguro == null)
		dr["CostoTotalSeguro"] = DBNull.Value;
		else
		dr["CostoTotalSeguro"] = CostoTotalSeguro;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		SolSaID = dr["sol_sa_ID"] != DBNull.Value ? Convert.ToInt32(dr["sol_sa_ID"]) : SolSaID = null;
		SolicitudID = dr["solicitudID"] != DBNull.Value ? Convert.ToInt32(dr["solicitudID"]) : SolicitudID = null;
		SeguroID = dr["seguroID"] != DBNull.Value ? Convert.ToInt32(dr["seguroID"]) : SeguroID = null;
		HectAseguradas = dr["hectAseguradas"] != DBNull.Value ? Convert.ToDouble(dr["hectAseguradas"]) : HectAseguradas = null;
		DescParcelas = dr["descParcelas"] != DBNull.Value ? Convert.ToString(dr["descParcelas"]) : DescParcelas = null;
		CostoTotalSeguro = dr["CostoTotalSeguro"] != DBNull.Value ? Convert.ToDouble(dr["CostoTotalSeguro"]) : CostoTotalSeguro = null;
	}
	
	public static SolicitudesSisBancosSeguroAgricola[] MapFrom(DataSet ds)
	{
		List<SolicitudesSisBancosSeguroAgricola> objects;
		
		
		// Initialise Collection.
		objects = new List<SolicitudesSisBancosSeguroAgricola>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[SolicitudesSisBancos_SeguroAgricola] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[SolicitudesSisBancos_SeguroAgricola] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			SolicitudesSisBancosSeguroAgricola instance = new SolicitudesSisBancosSeguroAgricola();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static SolicitudesSisBancosSeguroAgricola Get(System.Int32 solSaID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		SolicitudesSisBancosSeguroAgricola instance;
		
		
		instance = new SolicitudesSisBancosSeguroAgricola();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudesSisBancosSeguroAgricola_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, solSaID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get SolicitudesSisBancosSeguroAgricola ID:" + solSaID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? solicitudID, System.Int32? seguroID, System.Double? hectAseguradas, System.String descParcelas, System.Double? costoTotalSeguro, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudesSisBancosSeguroAgricola_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, solicitudID, seguroID, hectAseguradas, descParcelas, costoTotalSeguro);
		
		if (transaction == null)
		this.SolSaID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.SolSaID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? solicitudID, System.Int32? seguroID, System.Double? hectAseguradas, System.String descParcelas, System.Double? costoTotalSeguro)
	{
		Insert(solicitudID, seguroID, hectAseguradas, descParcelas, costoTotalSeguro, null);
	}
	/// <summary>
	/// Insert current SolicitudesSisBancosSeguroAgricola to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(SolicitudID, SeguroID, HectAseguradas, DescParcelas, CostoTotalSeguro, transaction);
	}
	
	/// <summary>
	/// Insert current SolicitudesSisBancosSeguroAgricola to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? solSaID, System.Int32? solicitudID, System.Int32? seguroID, System.Double? hectAseguradas, System.String descParcelas, System.Double? costoTotalSeguro, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudesSisBancosSeguroAgricola_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@solSaID"].Value = solSaID;
		dbCommand.Parameters["@solicitudID"].Value = solicitudID;
		dbCommand.Parameters["@seguroID"].Value = seguroID;
		dbCommand.Parameters["@hectAseguradas"].Value = hectAseguradas;
		dbCommand.Parameters["@descParcelas"].Value = descParcelas;
		dbCommand.Parameters["@costoTotalSeguro"].Value = costoTotalSeguro;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? solSaID, System.Int32? solicitudID, System.Int32? seguroID, System.Double? hectAseguradas, System.String descParcelas, System.Double? costoTotalSeguro)
	{
		Update(solSaID, solicitudID, seguroID, hectAseguradas, descParcelas, costoTotalSeguro, null);
	}
	
	public static void Update(SolicitudesSisBancosSeguroAgricola solicitudesSisBancosSeguroAgricola)
	{
		solicitudesSisBancosSeguroAgricola.Update();
	}
	
	public static void Update(SolicitudesSisBancosSeguroAgricola solicitudesSisBancosSeguroAgricola, DbTransaction transaction)
	{
		solicitudesSisBancosSeguroAgricola.Update(transaction);
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
		sqlCommand = "[dbo].gspSolicitudesSisBancosSeguroAgricola_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@solSaID"].SourceColumn = "sol_sa_ID";
		dbCommand.Parameters["@solicitudID"].SourceColumn = "solicitudID";
		dbCommand.Parameters["@seguroID"].SourceColumn = "seguroID";
		dbCommand.Parameters["@hectAseguradas"].SourceColumn = "hectAseguradas";
		dbCommand.Parameters["@descParcelas"].SourceColumn = "descParcelas";
		dbCommand.Parameters["@costoTotalSeguro"].SourceColumn = "CostoTotalSeguro";
		
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
	public static void Delete(System.Int32? solSaID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudesSisBancosSeguroAgricola_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, solSaID);
		
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
	public static void Delete(System.Int32? solSaID)
	{
		Delete(
		solSaID);
	}
	
	/// <summary>
	/// Delete current SolicitudesSisBancosSeguroAgricola from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudesSisBancosSeguroAgricola_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, SolSaID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.SolSaID = null;
	}
	
	/// <summary>
	/// Delete current SolicitudesSisBancosSeguroAgricola from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static SolicitudesSisBancosSeguroAgricola[] Search(System.Int32? solSaID, System.Int32? solicitudID, System.Int32? seguroID, System.Double? hectAseguradas, System.String descParcelas, System.Double? costoTotalSeguro)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudesSisBancosSeguroAgricola_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, solSaID, solicitudID, seguroID, hectAseguradas, descParcelas, costoTotalSeguro);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return SolicitudesSisBancosSeguroAgricola.MapFrom(ds);
	}
	
	
	public static SolicitudesSisBancosSeguroAgricola[] Search(SolicitudesSisBancosSeguroAgricola searchObject)
	{
		return Search ( searchObject.SolSaID, searchObject.SolicitudID, searchObject.SeguroID, searchObject.HectAseguradas, searchObject.DescParcelas, searchObject.CostoTotalSeguro);
	}
	
	/// <summary>
	/// Returns all SolicitudesSisBancosSeguroAgricola objects.
	/// </summary>
	/// <returns>List of all SolicitudesSisBancosSeguroAgricola objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static SolicitudesSisBancosSeguroAgricola[] Search()
	{
		return Search ( null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

