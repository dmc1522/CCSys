using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LasMargaritas.DL
{
    public class LastModificationDL
    {
        public string ConnectionString { get; set; }
        public LastModificationDL(string connectionString)
        {
            ConnectionString = connectionString;          
        }

        public LastModification GetLastModification(Module module)
        {
            LastModification lastModification = new LastModification();
            lastModification.Timestamp = null;
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetLastModification";
                    command.Parameters.Add("@ModuleId", SqlDbType.Int).Value = module;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                    {                        
                        lastModification.Module = (Module)int.Parse(reader["ModuleId"].ToString());
                        lastModification.Timestamp = reader["Timestamp"] != null ? DateTime.Parse(reader["Timestamp"].ToString()) : (DateTime?)null;
                    }
                    connection.Close();
                }                
            }
            return lastModification;
        }

        public bool SetLastModification(LastModification lastModification)
        {
            bool success = false;
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spSetLastModification";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ModuleId", SqlDbType.Int).Value = lastModification.Module;                        
                    command.Parameters.Add("@Timestamp", SqlDbType.DateTime).Value = DateTime.Now; //ignore the value
                    connection.Open();
                    object id = command.ExecuteScalar();
                    success = (int)id > 0;         
                    connection.Close();
                }
                return success;
            }
        }
    }
}
