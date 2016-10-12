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
public partial class FacturaDeProveedor
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[FacturaDeProveedor]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _facturaid;
	private System.Int32? _proveedorID;
	private System.String _numFactura;
	private System.DateTime? _fecha;
	private System.Decimal? _iva;
	private System.Double? _descuento;
	private System.Int32? _cicloId;
	private System.Int32? _tipomonedaid;
	private System.String _observaciones;
	
	#endregion
	
	
	#region Properties
	public System.Int32? Facturaid
	{
		get
		{
			return _facturaid;
		}
		set
		{
			_facturaid = value;
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
	
	public System.String NumFactura
	{
		get
		{
			return _numFactura;
		}
		set
		{
			_numFactura = value;
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
	
	public System.Decimal? Iva
	{
		get
		{
			return _iva;
		}
		set
		{
			_iva = value;
		}
	}
	
	public System.Double? Descuento
	{
		get
		{
			return _descuento;
		}
		set
		{
			_descuento = value;
		}
	}
	
	public System.Int32? CicloId
	{
		get
		{
			return _cicloId;
		}
		set
		{
			_cicloId = value;
		}
	}
	
	public System.Int32? Tipomonedaid
	{
		get
		{
			return _tipomonedaid;
		}
		set
		{
			_tipomonedaid = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("facturaid", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("proveedorID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("numFactura", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("fecha", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("IVA", typeof(System.Decimal) );
		ds.Tables[TABLE_NAME].Columns.Add("descuento", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("cicloId", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("tipomonedaid", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("observaciones", typeof(System.String) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (Facturaid == null)
		dr["facturaid"] = DBNull.Value;
		else
		dr["facturaid"] = Facturaid;
		
		if (ProveedorID == null)
		dr["proveedorID"] = DBNull.Value;
		else
		dr["proveedorID"] = ProveedorID;
		
		if (NumFactura == null)
		dr["numFactura"] = DBNull.Value;
		else
		dr["numFactura"] = NumFactura;
		
		if (Fecha == null)
		dr["fecha"] = DBNull.Value;
		else
		dr["fecha"] = Fecha;
		
		if (Iva == null)
		dr["IVA"] = DBNull.Value;
		else
		dr["IVA"] = Iva;
		
		if (Descuento == null)
		dr["descuento"] = DBNull.Value;
		else
		dr["descuento"] = Descuento;
		
		if (CicloId == null)
		dr["cicloId"] = DBNull.Value;
		else
		dr["cicloId"] = CicloId;
		
		if (Tipomonedaid == null)
		dr["tipomonedaid"] = DBNull.Value;
		else
		dr["tipomonedaid"] = Tipomonedaid;
		
		if (Observaciones == null)
		dr["observaciones"] = DBNull.Value;
		else
		dr["observaciones"] = Observaciones;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		Facturaid = dr["facturaid"] != DBNull.Value ? Convert.ToInt32(dr["facturaid"]) : Facturaid = null;
		ProveedorID = dr["proveedorID"] != DBNull.Value ? Convert.ToInt32(dr["proveedorID"]) : ProveedorID = null;
		NumFactura = dr["numFactura"] != DBNull.Value ? Convert.ToString(dr["numFactura"]) : NumFactura = null;
		Fecha = dr["fecha"] != DBNull.Value ? Convert.ToDateTime(dr["fecha"]) : Fecha = null;
		Iva = dr["IVA"] != DBNull.Value ? Convert.ToDecimal(dr["IVA"]) : Iva = null;
		Descuento = dr["descuento"] != DBNull.Value ? Convert.ToDouble(dr["descuento"]) : Descuento = null;
		CicloId = dr["cicloId"] != DBNull.Value ? Convert.ToInt32(dr["cicloId"]) : CicloId = null;
		Tipomonedaid = dr["tipomonedaid"] != DBNull.Value ? Convert.ToInt32(dr["tipomonedaid"]) : Tipomonedaid = null;
		Observaciones = dr["observaciones"] != DBNull.Value ? Convert.ToString(dr["observaciones"]) : Observaciones = null;
	}
	
	public static FacturaDeProveedor[] MapFrom(DataSet ds)
	{
		List<FacturaDeProveedor> objects;
		
		
		// Initialise Collection.
		objects = new List<FacturaDeProveedor>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[FacturaDeProveedor] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[FacturaDeProveedor] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			FacturaDeProveedor instance = new FacturaDeProveedor();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static FacturaDeProveedor Get(System.Int32 facturaid)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		FacturaDeProveedor instance;
		
		
		instance = new FacturaDeProveedor();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedor_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, facturaid);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get FacturaDeProveedor ID:" + facturaid.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? proveedorID, System.String numFactura, System.DateTime? fecha, System.Decimal? iva, System.Double? descuento, System.Int32? cicloId, System.Int32? tipomonedaid, System.String observaciones, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedor_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, proveedorID, numFactura, fecha, iva, descuento, cicloId, tipomonedaid, observaciones);
		
		if (transaction == null)
		this.Facturaid = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.Facturaid = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? proveedorID, System.String numFactura, System.DateTime? fecha, System.Decimal? iva, System.Double? descuento, System.Int32? cicloId, System.Int32? tipomonedaid, System.String observaciones)
	{
		Insert(proveedorID, numFactura, fecha, iva, descuento, cicloId, tipomonedaid, observaciones, null);
	}
	/// <summary>
	/// Insert current FacturaDeProveedor to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(ProveedorID, NumFactura, Fecha, Iva, Descuento, CicloId, Tipomonedaid, Observaciones, transaction);
	}
	
	/// <summary>
	/// Insert current FacturaDeProveedor to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? facturaid, System.Int32? proveedorID, System.String numFactura, System.DateTime? fecha, System.Decimal? iva, System.Double? descuento, System.Int32? cicloId, System.Int32? tipomonedaid, System.String observaciones, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedor_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@facturaid"].Value = facturaid;
		dbCommand.Parameters["@proveedorID"].Value = proveedorID;
		dbCommand.Parameters["@numFactura"].Value = numFactura;
		dbCommand.Parameters["@fecha"].Value = fecha;
		dbCommand.Parameters["@iva"].Value = iva;
		dbCommand.Parameters["@descuento"].Value = descuento;
		dbCommand.Parameters["@cicloId"].Value = cicloId;
		dbCommand.Parameters["@tipomonedaid"].Value = tipomonedaid;
		dbCommand.Parameters["@observaciones"].Value = observaciones;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? facturaid, System.Int32? proveedorID, System.String numFactura, System.DateTime? fecha, System.Decimal? iva, System.Double? descuento, System.Int32? cicloId, System.Int32? tipomonedaid, System.String observaciones)
	{
		Update(facturaid, proveedorID, numFactura, fecha, iva, descuento, cicloId, tipomonedaid, observaciones, null);
	}
	
	public static void Update(FacturaDeProveedor facturaDeProveedor)
	{
		facturaDeProveedor.Update();
	}
	
	public static void Update(FacturaDeProveedor facturaDeProveedor, DbTransaction transaction)
	{
		facturaDeProveedor.Update(transaction);
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
		sqlCommand = "[dbo].gspFacturaDeProveedor_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@facturaid"].SourceColumn = "facturaid";
		dbCommand.Parameters["@proveedorID"].SourceColumn = "proveedorID";
		dbCommand.Parameters["@numFactura"].SourceColumn = "numFactura";
		dbCommand.Parameters["@fecha"].SourceColumn = "fecha";
		dbCommand.Parameters["@iva"].SourceColumn = "IVA";
		dbCommand.Parameters["@descuento"].SourceColumn = "descuento";
		dbCommand.Parameters["@cicloId"].SourceColumn = "cicloId";
		dbCommand.Parameters["@tipomonedaid"].SourceColumn = "tipomonedaid";
		dbCommand.Parameters["@observaciones"].SourceColumn = "observaciones";
		
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
	public static void Delete(System.Int32? facturaid, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedor_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, facturaid);
		
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
	public static void Delete(System.Int32? facturaid)
	{
		Delete(
		facturaid);
	}
	
	/// <summary>
	/// Delete current FacturaDeProveedor from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedor_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, Facturaid);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.Facturaid = null;
	}
	
	/// <summary>
	/// Delete current FacturaDeProveedor from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static FacturaDeProveedor[] Search(System.Int32? facturaid, System.Int32? proveedorID, System.String numFactura, System.DateTime? fecha, System.Decimal? iva, System.Double? descuento, System.Int32? cicloId, System.Int32? tipomonedaid, System.String observaciones)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedor_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, facturaid, proveedorID, numFactura, fecha, iva, descuento, cicloId, tipomonedaid, observaciones);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return FacturaDeProveedor.MapFrom(ds);
	}
	
	
	public static FacturaDeProveedor[] Search(FacturaDeProveedor searchObject)
	{
		return Search ( searchObject.Facturaid, searchObject.ProveedorID, searchObject.NumFactura, searchObject.Fecha, searchObject.Iva, searchObject.Descuento, searchObject.CicloId, searchObject.Tipomonedaid, searchObject.Observaciones);
	}
	
	/// <summary>
	/// Returns all FacturaDeProveedor objects.
	/// </summary>
	/// <returns>List of all FacturaDeProveedor objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static FacturaDeProveedor[] Search()
	{
		return Search ( null, null, null, null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

