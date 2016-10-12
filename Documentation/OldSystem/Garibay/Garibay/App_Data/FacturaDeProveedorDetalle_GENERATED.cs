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
public partial class FacturaDeProveedorDetalle
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[FacturaDeProveedor_Detalle]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _facturaProveedordetalleID;
	private System.Int32? _facturaid;
	private System.Int32? _productoID;
	private System.Double? _precio;
	private System.Double? _cantidad;
	private System.Int32? _bodegaID;
	
	#endregion
	
	
	#region Properties
	public System.Int32? FacturaProveedordetalleID
	{
		get
		{
			return _facturaProveedordetalleID;
		}
		set
		{
			_facturaProveedordetalleID = value;
		}
	}
	
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
	
	public System.Double? Precio
	{
		get
		{
			return _precio;
		}
		set
		{
			_precio = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("facturaProveedordetalleID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("facturaid", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("productoID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("precio", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("cantidad", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("bodegaID", typeof(System.Int32) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (FacturaProveedordetalleID == null)
		dr["facturaProveedordetalleID"] = DBNull.Value;
		else
		dr["facturaProveedordetalleID"] = FacturaProveedordetalleID;
		
		if (Facturaid == null)
		dr["facturaid"] = DBNull.Value;
		else
		dr["facturaid"] = Facturaid;
		
		if (ProductoID == null)
		dr["productoID"] = DBNull.Value;
		else
		dr["productoID"] = ProductoID;
		
		if (Precio == null)
		dr["precio"] = DBNull.Value;
		else
		dr["precio"] = Precio;
		
		if (Cantidad == null)
		dr["cantidad"] = DBNull.Value;
		else
		dr["cantidad"] = Cantidad;
		
		if (BodegaID == null)
		dr["bodegaID"] = DBNull.Value;
		else
		dr["bodegaID"] = BodegaID;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		FacturaProveedordetalleID = dr["facturaProveedordetalleID"] != DBNull.Value ? Convert.ToInt32(dr["facturaProveedordetalleID"]) : FacturaProveedordetalleID = null;
		Facturaid = dr["facturaid"] != DBNull.Value ? Convert.ToInt32(dr["facturaid"]) : Facturaid = null;
		ProductoID = dr["productoID"] != DBNull.Value ? Convert.ToInt32(dr["productoID"]) : ProductoID = null;
		Precio = dr["precio"] != DBNull.Value ? Convert.ToDouble(dr["precio"]) : Precio = null;
		Cantidad = dr["cantidad"] != DBNull.Value ? Convert.ToDouble(dr["cantidad"]) : Cantidad = null;
		BodegaID = dr["bodegaID"] != DBNull.Value ? Convert.ToInt32(dr["bodegaID"]) : BodegaID = null;
	}
	
	public static FacturaDeProveedorDetalle[] MapFrom(DataSet ds)
	{
		List<FacturaDeProveedorDetalle> objects;
		
		
		// Initialise Collection.
		objects = new List<FacturaDeProveedorDetalle>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[FacturaDeProveedor_Detalle] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[FacturaDeProveedor_Detalle] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			FacturaDeProveedorDetalle instance = new FacturaDeProveedorDetalle();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static FacturaDeProveedorDetalle Get(System.Int32 facturaProveedordetalleID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		FacturaDeProveedorDetalle instance;
		
		
		instance = new FacturaDeProveedorDetalle();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedorDetalle_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, facturaProveedordetalleID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get FacturaDeProveedorDetalle ID:" + facturaProveedordetalleID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? facturaid, System.Int32? productoID, System.Double? precio, System.Double? cantidad, System.Int32? bodegaID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedorDetalle_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, facturaid, productoID, precio, cantidad, bodegaID);
		
		if (transaction == null)
		this.FacturaProveedordetalleID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.FacturaProveedordetalleID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? facturaid, System.Int32? productoID, System.Double? precio, System.Double? cantidad, System.Int32? bodegaID)
	{
		Insert(facturaid, productoID, precio, cantidad, bodegaID, null);
	}
	/// <summary>
	/// Insert current FacturaDeProveedorDetalle to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(Facturaid, ProductoID, Precio, Cantidad, BodegaID, transaction);
	}
	
	/// <summary>
	/// Insert current FacturaDeProveedorDetalle to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? facturaProveedordetalleID, System.Int32? facturaid, System.Int32? productoID, System.Double? precio, System.Double? cantidad, System.Int32? bodegaID, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedorDetalle_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@facturaProveedordetalleID"].Value = facturaProveedordetalleID;
		dbCommand.Parameters["@facturaid"].Value = facturaid;
		dbCommand.Parameters["@productoID"].Value = productoID;
		dbCommand.Parameters["@precio"].Value = precio;
		dbCommand.Parameters["@cantidad"].Value = cantidad;
		dbCommand.Parameters["@bodegaID"].Value = bodegaID;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? facturaProveedordetalleID, System.Int32? facturaid, System.Int32? productoID, System.Double? precio, System.Double? cantidad, System.Int32? bodegaID)
	{
		Update(facturaProveedordetalleID, facturaid, productoID, precio, cantidad, bodegaID, null);
	}
	
	public static void Update(FacturaDeProveedorDetalle facturaDeProveedorDetalle)
	{
		facturaDeProveedorDetalle.Update();
	}
	
	public static void Update(FacturaDeProveedorDetalle facturaDeProveedorDetalle, DbTransaction transaction)
	{
		facturaDeProveedorDetalle.Update(transaction);
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
		sqlCommand = "[dbo].gspFacturaDeProveedorDetalle_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@facturaProveedordetalleID"].SourceColumn = "facturaProveedordetalleID";
		dbCommand.Parameters["@facturaid"].SourceColumn = "facturaid";
		dbCommand.Parameters["@productoID"].SourceColumn = "productoID";
		dbCommand.Parameters["@precio"].SourceColumn = "precio";
		dbCommand.Parameters["@cantidad"].SourceColumn = "cantidad";
		dbCommand.Parameters["@bodegaID"].SourceColumn = "bodegaID";
		
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
	public static void Delete(System.Int32? facturaProveedordetalleID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedorDetalle_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, facturaProveedordetalleID);
		
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
	public static void Delete(System.Int32? facturaProveedordetalleID)
	{
		Delete(
		facturaProveedordetalleID);
	}
	
	/// <summary>
	/// Delete current FacturaDeProveedorDetalle from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedorDetalle_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, FacturaProveedordetalleID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.FacturaProveedordetalleID = null;
	}
	
	/// <summary>
	/// Delete current FacturaDeProveedorDetalle from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static FacturaDeProveedorDetalle[] Search(System.Int32? facturaProveedordetalleID, System.Int32? facturaid, System.Int32? productoID, System.Double? precio, System.Double? cantidad, System.Int32? bodegaID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspFacturaDeProveedorDetalle_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, facturaProveedordetalleID, facturaid, productoID, precio, cantidad, bodegaID);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return FacturaDeProveedorDetalle.MapFrom(ds);
	}
	
	
	public static FacturaDeProveedorDetalle[] Search(FacturaDeProveedorDetalle searchObject)
	{
		return Search ( searchObject.FacturaProveedordetalleID, searchObject.Facturaid, searchObject.ProductoID, searchObject.Precio, searchObject.Cantidad, searchObject.BodegaID);
	}
	
	/// <summary>
	/// Returns all FacturaDeProveedorDetalle objects.
	/// </summary>
	/// <returns>List of all FacturaDeProveedorDetalle objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static FacturaDeProveedorDetalle[] Search()
	{
		return Search ( null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

