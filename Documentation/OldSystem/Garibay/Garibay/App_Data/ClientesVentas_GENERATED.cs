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
public partial class ClientesVentas
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[ClientesVentas]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _clienteventaID;
	private System.String _nombre;
	private System.String _domicilio;
	private System.String _ciudad;
	private System.String _telefono;
	private System.Int32? _estadoID;
	private System.String _rfc;
	private System.Int32? _userID;
	private System.DateTime? _storeTS;
	private System.String _colonia;
	private System.String _cp;
	private System.DateTime? _lastEditDate;
	private System.DateTime? _creationDate;
	
	#endregion
	
	
	#region Properties
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
	
	public System.String Ciudad
	{
		get
		{
			return _ciudad;
		}
		set
		{
			_ciudad = value;
		}
	}
	
	public System.String Telefono
	{
		get
		{
			return _telefono;
		}
		set
		{
			_telefono = value;
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
	
	public System.String Rfc
	{
		get
		{
			return _rfc;
		}
		set
		{
			_rfc = value;
		}
	}
	
	public System.Int32? UserID
	{
		get
		{
			return _userID;
		}
		set
		{
			_userID = value;
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
	
	public System.String Colonia
	{
		get
		{
			return _colonia;
		}
		set
		{
			_colonia = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("clienteventaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("nombre", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("domicilio", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("ciudad", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("telefono", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("estadoID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("RFC", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("userID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("storeTS", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("colonia", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("CP", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("LastEditDate", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("CreationDate", typeof(System.DateTime) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (ClienteventaID == null)
		dr["clienteventaID"] = DBNull.Value;
		else
		dr["clienteventaID"] = ClienteventaID;
		
		if (Nombre == null)
		dr["nombre"] = DBNull.Value;
		else
		dr["nombre"] = Nombre;
		
		if (Domicilio == null)
		dr["domicilio"] = DBNull.Value;
		else
		dr["domicilio"] = Domicilio;
		
		if (Ciudad == null)
		dr["ciudad"] = DBNull.Value;
		else
		dr["ciudad"] = Ciudad;
		
		if (Telefono == null)
		dr["telefono"] = DBNull.Value;
		else
		dr["telefono"] = Telefono;
		
		if (EstadoID == null)
		dr["estadoID"] = DBNull.Value;
		else
		dr["estadoID"] = EstadoID;
		
		if (Rfc == null)
		dr["RFC"] = DBNull.Value;
		else
		dr["RFC"] = Rfc;
		
		if (UserID == null)
		dr["userID"] = DBNull.Value;
		else
		dr["userID"] = UserID;
		
		if (StoreTS == null)
		dr["storeTS"] = DBNull.Value;
		else
		dr["storeTS"] = StoreTS;
		
		if (Colonia == null)
		dr["colonia"] = DBNull.Value;
		else
		dr["colonia"] = Colonia;
		
		if (Cp == null)
		dr["CP"] = DBNull.Value;
		else
		dr["CP"] = Cp;
		
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
		ClienteventaID = dr["clienteventaID"] != DBNull.Value ? Convert.ToInt32(dr["clienteventaID"]) : ClienteventaID = null;
		Nombre = dr["nombre"] != DBNull.Value ? Convert.ToString(dr["nombre"]) : Nombre = null;
		Domicilio = dr["domicilio"] != DBNull.Value ? Convert.ToString(dr["domicilio"]) : Domicilio = null;
		Ciudad = dr["ciudad"] != DBNull.Value ? Convert.ToString(dr["ciudad"]) : Ciudad = null;
		Telefono = dr["telefono"] != DBNull.Value ? Convert.ToString(dr["telefono"]) : Telefono = null;
		EstadoID = dr["estadoID"] != DBNull.Value ? Convert.ToInt32(dr["estadoID"]) : EstadoID = null;
		Rfc = dr["RFC"] != DBNull.Value ? Convert.ToString(dr["RFC"]) : Rfc = null;
		UserID = dr["userID"] != DBNull.Value ? Convert.ToInt32(dr["userID"]) : UserID = null;
		StoreTS = dr["storeTS"] != DBNull.Value ? Convert.ToDateTime(dr["storeTS"]) : StoreTS = null;
		Colonia = dr["colonia"] != DBNull.Value ? Convert.ToString(dr["colonia"]) : Colonia = null;
		Cp = dr["CP"] != DBNull.Value ? Convert.ToString(dr["CP"]) : Cp = null;
		LastEditDate = dr["LastEditDate"] != DBNull.Value ? Convert.ToDateTime(dr["LastEditDate"]) : LastEditDate = null;
		CreationDate = dr["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreationDate"]) : CreationDate = null;
	}
	
	public static ClientesVentas[] MapFrom(DataSet ds)
	{
		List<ClientesVentas> objects;
		
		
		// Initialise Collection.
		objects = new List<ClientesVentas>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[ClientesVentas] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[ClientesVentas] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			ClientesVentas instance = new ClientesVentas();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static ClientesVentas Get(System.Int32 clienteventaID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		ClientesVentas instance;
		
		
		instance = new ClientesVentas();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClientesVentas_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, clienteventaID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get ClientesVentas ID:" + clienteventaID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.String nombre, System.String domicilio, System.String ciudad, System.String telefono, System.Int32? estadoID, System.String rfc, System.Int32? userID, System.DateTime? storeTS, System.String colonia, System.String cp, System.DateTime? lastEditDate, System.DateTime? creationDate, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClientesVentas_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, nombre, domicilio, ciudad, telefono, estadoID, rfc, userID, storeTS, colonia, cp, lastEditDate, creationDate);
		
		if (transaction == null)
		this.ClienteventaID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.ClienteventaID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.String nombre, System.String domicilio, System.String ciudad, System.String telefono, System.Int32? estadoID, System.String rfc, System.Int32? userID, System.DateTime? storeTS, System.String colonia, System.String cp, System.DateTime? lastEditDate, System.DateTime? creationDate)
	{
		Insert(nombre, domicilio, ciudad, telefono, estadoID, rfc, userID, storeTS, colonia, cp, lastEditDate, creationDate, null);
	}
	/// <summary>
	/// Insert current ClientesVentas to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(Nombre, Domicilio, Ciudad, Telefono, EstadoID, Rfc, UserID, StoreTS, Colonia, Cp, LastEditDate, CreationDate, transaction);
	}
	
	/// <summary>
	/// Insert current ClientesVentas to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? clienteventaID, System.String nombre, System.String domicilio, System.String ciudad, System.String telefono, System.Int32? estadoID, System.String rfc, System.Int32? userID, System.DateTime? storeTS, System.String colonia, System.String cp, System.DateTime? lastEditDate, System.DateTime? creationDate, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClientesVentas_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@clienteventaID"].Value = clienteventaID;
		dbCommand.Parameters["@nombre"].Value = nombre;
		dbCommand.Parameters["@domicilio"].Value = domicilio;
		dbCommand.Parameters["@ciudad"].Value = ciudad;
		dbCommand.Parameters["@telefono"].Value = telefono;
		dbCommand.Parameters["@estadoID"].Value = estadoID;
		dbCommand.Parameters["@rfc"].Value = rfc;
		dbCommand.Parameters["@userID"].Value = userID;
		dbCommand.Parameters["@storeTS"].Value = storeTS;
		dbCommand.Parameters["@colonia"].Value = colonia;
		dbCommand.Parameters["@cp"].Value = cp;
		dbCommand.Parameters["@lastEditDate"].Value = lastEditDate;
		dbCommand.Parameters["@creationDate"].Value = creationDate;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? clienteventaID, System.String nombre, System.String domicilio, System.String ciudad, System.String telefono, System.Int32? estadoID, System.String rfc, System.Int32? userID, System.DateTime? storeTS, System.String colonia, System.String cp, System.DateTime? lastEditDate, System.DateTime? creationDate)
	{
		Update(clienteventaID, nombre, domicilio, ciudad, telefono, estadoID, rfc, userID, storeTS, colonia, cp, lastEditDate, creationDate, null);
	}
	
	public static void Update(ClientesVentas clientesVentas)
	{
		clientesVentas.Update();
	}
	
	public static void Update(ClientesVentas clientesVentas, DbTransaction transaction)
	{
		clientesVentas.Update(transaction);
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
		sqlCommand = "[dbo].gspClientesVentas_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@clienteventaID"].SourceColumn = "clienteventaID";
		dbCommand.Parameters["@nombre"].SourceColumn = "nombre";
		dbCommand.Parameters["@domicilio"].SourceColumn = "domicilio";
		dbCommand.Parameters["@ciudad"].SourceColumn = "ciudad";
		dbCommand.Parameters["@telefono"].SourceColumn = "telefono";
		dbCommand.Parameters["@estadoID"].SourceColumn = "estadoID";
		dbCommand.Parameters["@rfc"].SourceColumn = "RFC";
		dbCommand.Parameters["@userID"].SourceColumn = "userID";
		dbCommand.Parameters["@storeTS"].SourceColumn = "storeTS";
		dbCommand.Parameters["@colonia"].SourceColumn = "colonia";
		dbCommand.Parameters["@cp"].SourceColumn = "CP";
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
	public static void Delete(System.Int32? clienteventaID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClientesVentas_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, clienteventaID);
		
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
	public static void Delete(System.Int32? clienteventaID)
	{
		Delete(
		clienteventaID);
	}
	
	/// <summary>
	/// Delete current ClientesVentas from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClientesVentas_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, ClienteventaID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.ClienteventaID = null;
	}
	
	/// <summary>
	/// Delete current ClientesVentas from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static ClientesVentas[] Search(System.Int32? clienteventaID, System.String nombre, System.String domicilio, System.String ciudad, System.String telefono, System.Int32? estadoID, System.String rfc, System.Int32? userID, System.DateTime? storeTS, System.String colonia, System.String cp, System.DateTime? lastEditDate, System.DateTime? creationDate)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspClientesVentas_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, clienteventaID, nombre, domicilio, ciudad, telefono, estadoID, rfc, userID, storeTS, colonia, cp, lastEditDate, creationDate);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return ClientesVentas.MapFrom(ds);
	}
	
	
	public static ClientesVentas[] Search(ClientesVentas searchObject)
	{
		return Search ( searchObject.ClienteventaID, searchObject.Nombre, searchObject.Domicilio, searchObject.Ciudad, searchObject.Telefono, searchObject.EstadoID, searchObject.Rfc, searchObject.UserID, searchObject.StoreTS, searchObject.Colonia, searchObject.Cp, searchObject.LastEditDate, searchObject.CreationDate);
	}
	
	/// <summary>
	/// Returns all ClientesVentas objects.
	/// </summary>
	/// <returns>List of all ClientesVentas objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static ClientesVentas[] Search()
	{
		return Search ( null, null, null, null, null, null, null, null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

