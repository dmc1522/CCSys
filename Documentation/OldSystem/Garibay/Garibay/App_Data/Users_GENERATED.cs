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
public partial class Users
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[Users]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _userID;
	private System.String _username;
	private System.String _password;
	private System.Int32? _securitylevelID;
	private System.Boolean? _enabled;
	private System.String _nombre;
	private System.String _email;
	private System.DateTime? _lastEditDate;
	private System.DateTime? _creationDate;
	
	#endregion
	
	
	#region Properties
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
	
	public System.String Username
	{
		get
		{
			return _username;
		}
		set
		{
			_username = value;
		}
	}
	
	public System.String Password
	{
		get
		{
			return _password;
		}
		set
		{
			_password = value;
		}
	}
	
	public System.Int32? SecuritylevelID
	{
		get
		{
			return _securitylevelID;
		}
		set
		{
			_securitylevelID = value;
		}
	}
	
	public System.Boolean? Enabled
	{
		get
		{
			return _enabled;
		}
		set
		{
			_enabled = value;
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
	
	public System.String Email
	{
		get
		{
			return _email;
		}
		set
		{
			_email = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("userID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("username", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("password", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("securitylevelID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("enabled", typeof(System.Boolean) );
		ds.Tables[TABLE_NAME].Columns.Add("Nombre", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("email", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("LastEditDate", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("CreationDate", typeof(System.DateTime) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (UserID == null)
		dr["userID"] = DBNull.Value;
		else
		dr["userID"] = UserID;
		
		if (Username == null)
		dr["username"] = DBNull.Value;
		else
		dr["username"] = Username;
		
		if (Password == null)
		dr["password"] = DBNull.Value;
		else
		dr["password"] = Password;
		
		if (SecuritylevelID == null)
		dr["securitylevelID"] = DBNull.Value;
		else
		dr["securitylevelID"] = SecuritylevelID;
		
		if (Enabled == null)
		dr["enabled"] = DBNull.Value;
		else
		dr["enabled"] = Enabled;
		
		if (Nombre == null)
		dr["Nombre"] = DBNull.Value;
		else
		dr["Nombre"] = Nombre;
		
		if (Email == null)
		dr["email"] = DBNull.Value;
		else
		dr["email"] = Email;
		
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
		UserID = dr["userID"] != DBNull.Value ? Convert.ToInt32(dr["userID"]) : UserID = null;
		Username = dr["username"] != DBNull.Value ? Convert.ToString(dr["username"]) : Username = null;
		Password = dr["password"] != DBNull.Value ? Convert.ToString(dr["password"]) : Password = null;
		SecuritylevelID = dr["securitylevelID"] != DBNull.Value ? Convert.ToInt32(dr["securitylevelID"]) : SecuritylevelID = null;
		Enabled = dr["enabled"] != DBNull.Value ? Convert.ToBoolean(dr["enabled"]) : Enabled = null;
		Nombre = dr["Nombre"] != DBNull.Value ? Convert.ToString(dr["Nombre"]) : Nombre = null;
		Email = dr["email"] != DBNull.Value ? Convert.ToString(dr["email"]) : Email = null;
		LastEditDate = dr["LastEditDate"] != DBNull.Value ? Convert.ToDateTime(dr["LastEditDate"]) : LastEditDate = null;
		CreationDate = dr["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreationDate"]) : CreationDate = null;
	}
	
	public static Users[] MapFrom(DataSet ds)
	{
		List<Users> objects;
		
		
		// Initialise Collection.
		objects = new List<Users>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[Users] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[Users] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			Users instance = new Users();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Users Get(System.Int32 userID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		Users instance;
		
		
		instance = new Users();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspUsers_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, userID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get Users ID:" + userID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.String username, System.String password, System.Int32? securitylevelID, System.Boolean? enabled, System.String nombre, System.String email, System.DateTime? lastEditDate, System.DateTime? creationDate, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspUsers_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, username, password, securitylevelID, enabled, nombre, email, lastEditDate, creationDate);
		
		if (transaction == null)
		this.UserID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.UserID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.String username, System.String password, System.Int32? securitylevelID, System.Boolean? enabled, System.String nombre, System.String email, System.DateTime? lastEditDate, System.DateTime? creationDate)
	{
		Insert(username, password, securitylevelID, enabled, nombre, email, lastEditDate, creationDate, null);
	}
	/// <summary>
	/// Insert current Users to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(Username, Password, SecuritylevelID, Enabled, Nombre, Email, LastEditDate, CreationDate, transaction);
	}
	
	/// <summary>
	/// Insert current Users to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? userID, System.String username, System.String password, System.Int32? securitylevelID, System.Boolean? enabled, System.String nombre, System.String email, System.DateTime? lastEditDate, System.DateTime? creationDate, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspUsers_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@userID"].Value = userID;
		dbCommand.Parameters["@username"].Value = username;
		dbCommand.Parameters["@password"].Value = password;
		dbCommand.Parameters["@securitylevelID"].Value = securitylevelID;
		dbCommand.Parameters["@enabled"].Value = enabled;
		dbCommand.Parameters["@nombre"].Value = nombre;
		dbCommand.Parameters["@email"].Value = email;
		dbCommand.Parameters["@lastEditDate"].Value = lastEditDate;
		dbCommand.Parameters["@creationDate"].Value = creationDate;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? userID, System.String username, System.String password, System.Int32? securitylevelID, System.Boolean? enabled, System.String nombre, System.String email, System.DateTime? lastEditDate, System.DateTime? creationDate)
	{
		Update(userID, username, password, securitylevelID, enabled, nombre, email, lastEditDate, creationDate, null);
	}
	
	public static void Update(Users users)
	{
		users.Update();
	}
	
	public static void Update(Users users, DbTransaction transaction)
	{
		users.Update(transaction);
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
		sqlCommand = "[dbo].gspUsers_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@userID"].SourceColumn = "userID";
		dbCommand.Parameters["@username"].SourceColumn = "username";
		dbCommand.Parameters["@password"].SourceColumn = "password";
		dbCommand.Parameters["@securitylevelID"].SourceColumn = "securitylevelID";
		dbCommand.Parameters["@enabled"].SourceColumn = "enabled";
		dbCommand.Parameters["@nombre"].SourceColumn = "Nombre";
		dbCommand.Parameters["@email"].SourceColumn = "email";
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
	public static void Delete(System.Int32? userID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspUsers_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, userID);
		
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
	public static void Delete(System.Int32? userID)
	{
		Delete(
		userID);
	}
	
	/// <summary>
	/// Delete current Users from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspUsers_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, UserID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.UserID = null;
	}
	
	/// <summary>
	/// Delete current Users from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Users[] Search(System.Int32? userID, System.String username, System.String password, System.Int32? securitylevelID, System.Boolean? enabled, System.String nombre, System.String email, System.DateTime? lastEditDate, System.DateTime? creationDate)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspUsers_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, userID, username, password, securitylevelID, enabled, nombre, email, lastEditDate, creationDate);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return Users.MapFrom(ds);
	}
	
	
	public static Users[] Search(Users searchObject)
	{
		return Search ( searchObject.UserID, searchObject.Username, searchObject.Password, searchObject.SecuritylevelID, searchObject.Enabled, searchObject.Nombre, searchObject.Email, searchObject.LastEditDate, searchObject.CreationDate);
	}
	
	/// <summary>
	/// Returns all Users objects.
	/// </summary>
	/// <returns>List of all Users objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static Users[] Search()
	{
		return Search ( null, null, null, null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

