/****************************************************************************/
/* Code Author (written by Xin Zhao)                                        */
/*                                                                          */
/* This file was automatically generated using Code Author.                 */
/* Any manual changes to this file will be overwritten by a automated tool. */
/*                                                                          */
/* Date Generated: 14/05/2011 10:57:19 a.m.                                    */
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
public partial class Proveedores
{
	
	
	#region Constants
    private static readonly string TABLE_NAME = "Proveedores";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _proveedorID;
	private System.String _nombre;
	private System.String _direccion;
	private System.String _cp;
	private System.String _comunidad;
	private System.String _municipio;
	private System.Int32? _estadoID;
	private System.String _teléfono;
	private System.String _celular;
	private System.DateTime? _fechaalta;
	private System.String _nombrecontacto;
	private System.String _banco;
	private System.String _observaciones;
	private System.DateTime? _storeTS;
	private System.DateTime? _updateTS;
	
	#endregion
	
	
	#region Properties
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
	
	public System.String Nombre
	{
		get
		{
			return _nombre;
		}
		set
		{
			_nombre = value;
		}
	}
	
	public System.String Direccion
	{
		get
		{
			return _direccion;
		}
		set
		{
			_direccion = value;
		}
	}
	
	public System.String Cp
	{
		get
		{
			return _cp;
		}
		set
		{
			_cp = value;
		}
	}
	
	public System.String Comunidad
	{
		get
		{
			return _comunidad;
		}
		set
		{
			_comunidad = value;
		}
	}
	
	public System.String Municipio
	{
		get
		{
			return _municipio;
		}
		set
		{
			_municipio = value;
		}
	}
	
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
	
	public System.String Teléfono
	{
		get
		{
			return _teléfono;
		}
		set
		{
			_teléfono = value;
		}
	}
	
	public System.String Celular
	{
		get
		{
			return _celular;
		}
		set
		{
			_celular = value;
		}
	}
	
	public System.DateTime? Fechaalta
	{
		get
		{
			return _fechaalta;
		}
		set
		{
			_fechaalta = value;
		}
	}
	
	public System.String Nombrecontacto
	{
		get
		{
			return _nombrecontacto;
		}
		set
		{
			_nombrecontacto = value;
		}
	}
	
	public System.String Banco
	{
		get
		{
			return _banco;
		}
		set
		{
			_banco = value;
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
	
	public System.DateTime? StoreTS
	{
		get
		{
			return _storeTS;
		}
		set
		{
			_storeTS = value;
		}
	}
	
	public System.DateTime? UpdateTS
	{
		get
		{
			return _updateTS;
		}
		set
		{
			_updateTS = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("proveedorID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("Nombre", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("Direccion", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("CP", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("Comunidad", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("Municipio", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("estadoID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("Teléfono", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("Celular", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("Fechaalta", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("Nombrecontacto", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("banco", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("Observaciones", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("storeTS", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("updateTS", typeof(System.DateTime) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (ProveedorID == null)
		dr["proveedorID"] = DBNull.Value;
		else
		dr["proveedorID"] = ProveedorID;
		
		if (Nombre == null)
		dr["Nombre"] = DBNull.Value;
		else
		dr["Nombre"] = Nombre;
		
		if (Direccion == null)
		dr["Direccion"] = DBNull.Value;
		else
		dr["Direccion"] = Direccion;
		
		if (Cp == null)
		dr["CP"] = DBNull.Value;
		else
		dr["CP"] = Cp;
		
		if (Comunidad == null)
		dr["Comunidad"] = DBNull.Value;
		else
		dr["Comunidad"] = Comunidad;
		
		if (Municipio == null)
		dr["Municipio"] = DBNull.Value;
		else
		dr["Municipio"] = Municipio;
		
		if (EstadoID == null)
		dr["estadoID"] = DBNull.Value;
		else
		dr["estadoID"] = EstadoID;
		
		if (Teléfono == null)
		dr["Teléfono"] = DBNull.Value;
		else
		dr["Teléfono"] = Teléfono;
		
		if (Celular == null)
		dr["Celular"] = DBNull.Value;
		else
		dr["Celular"] = Celular;
		
		if (Fechaalta == null)
		dr["Fechaalta"] = DBNull.Value;
		else
		dr["Fechaalta"] = Fechaalta;
		
		if (Nombrecontacto == null)
		dr["Nombrecontacto"] = DBNull.Value;
		else
		dr["Nombrecontacto"] = Nombrecontacto;
		
		if (Banco == null)
		dr["banco"] = DBNull.Value;
		else
		dr["banco"] = Banco;
		
		if (Observaciones == null)
		dr["Observaciones"] = DBNull.Value;
		else
		dr["Observaciones"] = Observaciones;
		
		if (StoreTS == null)
		dr["storeTS"] = DBNull.Value;
		else
		dr["storeTS"] = StoreTS;
		
		if (UpdateTS == null)
		dr["updateTS"] = DBNull.Value;
		else
		dr["updateTS"] = UpdateTS;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		ProveedorID = dr["proveedorID"] != DBNull.Value ? Convert.ToInt32(dr["proveedorID"]) : ProveedorID = null;
		Nombre = dr["Nombre"] != DBNull.Value ? Convert.ToString(dr["Nombre"]) : Nombre = null;
		Direccion = dr["Direccion"] != DBNull.Value ? Convert.ToString(dr["Direccion"]) : Direccion = null;
		Cp = dr["CP"] != DBNull.Value ? Convert.ToString(dr["CP"]) : Cp = null;
		Comunidad = dr["Comunidad"] != DBNull.Value ? Convert.ToString(dr["Comunidad"]) : Comunidad = null;
		Municipio = dr["Municipio"] != DBNull.Value ? Convert.ToString(dr["Municipio"]) : Municipio = null;
		EstadoID = dr["estadoID"] != DBNull.Value ? Convert.ToInt32(dr["estadoID"]) : EstadoID = null;
		Teléfono = dr["Teléfono"] != DBNull.Value ? Convert.ToString(dr["Teléfono"]) : Teléfono = null;
		Celular = dr["Celular"] != DBNull.Value ? Convert.ToString(dr["Celular"]) : Celular = null;
		Fechaalta = dr["Fechaalta"] != DBNull.Value ? Convert.ToDateTime(dr["Fechaalta"]) : Fechaalta = null;
		Nombrecontacto = dr["Nombrecontacto"] != DBNull.Value ? Convert.ToString(dr["Nombrecontacto"]) : Nombrecontacto = null;
		Banco = dr["banco"] != DBNull.Value ? Convert.ToString(dr["banco"]) : Banco = null;
		Observaciones = dr["Observaciones"] != DBNull.Value ? Convert.ToString(dr["Observaciones"]) : Observaciones = null;
		StoreTS = dr["storeTS"] != DBNull.Value ? Convert.ToDateTime(dr["storeTS"]) : StoreTS = null;
		UpdateTS = dr["updateTS"] != DBNull.Value ? Convert.ToDateTime(dr["updateTS"]) : UpdateTS = null;
	}
	
	public static Proveedores[] MapFrom(DataSet ds)
	{
		List<Proveedores> objects;
		
		
		// Initialise Collection.
		objects = new List<Proveedores>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[Proveedores] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[Proveedores] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			Proveedores instance = new Proveedores();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Proveedores Get(System.Int32 proveedorID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		Proveedores instance;
		
		
		instance = new Proveedores();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspProveedores_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, proveedorID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get Proveedores ID:" + proveedorID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.String nombre, System.String direccion, System.String cp, System.String comunidad, System.String municipio, System.Int32? estadoID, System.String teléfono, System.String celular, System.DateTime? fechaalta, System.String nombrecontacto, System.String banco, System.String observaciones, System.DateTime? storeTS, System.DateTime? updateTS, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspProveedores_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, nombre, direccion, cp, comunidad, municipio, estadoID, teléfono, celular, fechaalta, nombrecontacto, banco, observaciones, storeTS, updateTS);
		
		if (transaction == null)
		this.ProveedorID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.ProveedorID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.String nombre, System.String direccion, System.String cp, System.String comunidad, System.String municipio, System.Int32? estadoID, System.String teléfono, System.String celular, System.DateTime? fechaalta, System.String nombrecontacto, System.String banco, System.String observaciones, System.DateTime? storeTS, System.DateTime? updateTS)
	{
		Insert(nombre, direccion, cp, comunidad, municipio, estadoID, teléfono, celular, fechaalta, nombrecontacto, banco, observaciones, storeTS, updateTS, null);
	}
	/// <summary>
	/// Insert current Proveedores to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(Nombre, Direccion, Cp, Comunidad, Municipio, EstadoID, Teléfono, Celular, Fechaalta, Nombrecontacto, Banco, Observaciones, StoreTS, UpdateTS, transaction);
	}
	
	/// <summary>
	/// Insert current Proveedores to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? proveedorID, System.String nombre, System.String direccion, System.String cp, System.String comunidad, System.String municipio, System.Int32? estadoID, System.String teléfono, System.String celular, System.DateTime? fechaalta, System.String nombrecontacto, System.String banco, System.String observaciones, System.DateTime? storeTS, System.DateTime? updateTS, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspProveedores_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@proveedorID"].Value = proveedorID;
		dbCommand.Parameters["@nombre"].Value = nombre;
		dbCommand.Parameters["@direccion"].Value = direccion;
		dbCommand.Parameters["@cp"].Value = cp;
		dbCommand.Parameters["@comunidad"].Value = comunidad;
		dbCommand.Parameters["@municipio"].Value = municipio;
		dbCommand.Parameters["@estadoID"].Value = estadoID;
		dbCommand.Parameters["@teléfono"].Value = teléfono;
		dbCommand.Parameters["@celular"].Value = celular;
		dbCommand.Parameters["@fechaalta"].Value = fechaalta;
		dbCommand.Parameters["@nombrecontacto"].Value = nombrecontacto;
		dbCommand.Parameters["@banco"].Value = banco;
		dbCommand.Parameters["@observaciones"].Value = observaciones;
		dbCommand.Parameters["@storeTS"].Value = storeTS;
		dbCommand.Parameters["@updateTS"].Value = updateTS;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? proveedorID, System.String nombre, System.String direccion, System.String cp, System.String comunidad, System.String municipio, System.Int32? estadoID, System.String teléfono, System.String celular, System.DateTime? fechaalta, System.String nombrecontacto, System.String banco, System.String observaciones, System.DateTime? storeTS, System.DateTime? updateTS)
	{
		Update(proveedorID, nombre, direccion, cp, comunidad, municipio, estadoID, teléfono, celular, fechaalta, nombrecontacto, banco, observaciones, storeTS, updateTS, null);
	}
	
	public static void Update(Proveedores proveedores)
	{
		proveedores.Update();
	}
	
	public static void Update(Proveedores proveedores, DbTransaction transaction)
	{
		proveedores.Update(transaction);
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
		sqlCommand = "[dbo].gspProveedores_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@proveedorID"].SourceColumn = "proveedorID";
		dbCommand.Parameters["@nombre"].SourceColumn = "Nombre";
		dbCommand.Parameters["@direccion"].SourceColumn = "Direccion";
		dbCommand.Parameters["@cp"].SourceColumn = "CP";
		dbCommand.Parameters["@comunidad"].SourceColumn = "Comunidad";
		dbCommand.Parameters["@municipio"].SourceColumn = "Municipio";
		dbCommand.Parameters["@estadoID"].SourceColumn = "estadoID";
		dbCommand.Parameters["@teléfono"].SourceColumn = "Teléfono";
		dbCommand.Parameters["@celular"].SourceColumn = "Celular";
		dbCommand.Parameters["@fechaalta"].SourceColumn = "Fechaalta";
		dbCommand.Parameters["@nombrecontacto"].SourceColumn = "Nombrecontacto";
		dbCommand.Parameters["@banco"].SourceColumn = "banco";
		dbCommand.Parameters["@observaciones"].SourceColumn = "Observaciones";
		dbCommand.Parameters["@storeTS"].SourceColumn = "storeTS";
		dbCommand.Parameters["@updateTS"].SourceColumn = "updateTS";
		
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
	public static void Delete(System.Int32? proveedorID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspProveedores_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, proveedorID);
		
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
	public static void Delete(System.Int32? proveedorID)
	{
		Delete(
		proveedorID);
	}
	
	/// <summary>
	/// Delete current Proveedores from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspProveedores_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, ProveedorID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.ProveedorID = null;
	}
	
	/// <summary>
	/// Delete current Proveedores from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Proveedores[] Search(System.Int32? proveedorID, System.String nombre, System.String direccion, System.String cp, System.String comunidad, System.String municipio, System.Int32? estadoID, System.String teléfono, System.String celular, System.DateTime? fechaalta, System.String nombrecontacto, System.String banco, System.String observaciones, System.DateTime? storeTS, System.DateTime? updateTS)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspProveedores_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, proveedorID, nombre, direccion, cp, comunidad, municipio, estadoID, teléfono, celular, fechaalta, nombrecontacto, banco, observaciones, storeTS, updateTS);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return Proveedores.MapFrom(ds);
	}
	
	
	public static Proveedores[] Search(Proveedores searchObject)
	{
		return Search ( searchObject.ProveedorID, searchObject.Nombre, searchObject.Direccion, searchObject.Cp, searchObject.Comunidad, searchObject.Municipio, searchObject.EstadoID, searchObject.Teléfono, searchObject.Celular, searchObject.Fechaalta, searchObject.Nombrecontacto, searchObject.Banco, searchObject.Observaciones, searchObject.StoreTS, searchObject.UpdateTS);
	}
	
	/// <summary>
	/// Returns all Proveedores objects.
	/// </summary>
	/// <returns>List of all Proveedores objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static Proveedores[] Search()
	{
		return Search ( null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

