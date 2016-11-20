using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace LasMargaritas.DL
{
    public class RancherDL
    {
        public string ConnectionString { get; set; }
        public RancherDL(string connectionString)
        {
            ConnectionString = connectionString;
            excludedPropertiesInInsert = new List<string>();
            excludedPropertiesInUpdate = new List<string>();

            //excluding these while inserting
            excludedPropertiesInInsert.Add("Id");
            excludedPropertiesInInsert.Add("StoreTS");
            excludedPropertiesInInsert.Add("UpdateTS");


            //exluding these while updating
            excludedPropertiesInUpdate.Add("StoreTS");
            excludedPropertiesInUpdate.Add("UpdateTS");
        }

        private List<string> excludedPropertiesInInsert;

        private List<string> excludedPropertiesInUpdate;

        public Rancher InsertRancher(Rancher rancher)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertRancher";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in rancher.GetType().GetProperties() where !excludedPropertiesInInsert.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(rancher));
                    }
                    connection.Open();
                    object rancherId = command.ExecuteScalar();
                    rancher.Id = int.Parse(rancherId.ToString());
                    connection.Close();
                }
                return rancher;
            }
        }

        public Rancher UpdateRancher(Rancher rancher)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertRancher";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in rancher.GetType().GetProperties() where !excludedPropertiesInUpdate.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(rancher));
                    }
                    connection.Open();
                    object rancherId = command.ExecuteScalar();
                    rancher.Id = int.Parse(rancherId.ToString());
                    connection.Close();
                }
                return rancher;
            }
        }

        public List<Rancher> GetRancher(int? id = null)
        {
            List<Rancher> producers = new List<Rancher>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetRancher";
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    producers = DataReaderMapper.Map<Rancher>(reader);
                    reader.Close();
                    connection.Close();
                }
                return producers;
            }
        }

        public List<SelectableModel> GetBasicModels()
        {
            List<SelectableModel> ranchers = new List<SelectableModel>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetRancherSelectableModels";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    ranchers = DataReaderMapper.Map<SelectableModel>(reader);
                    reader.Close();
                    connection.Close();
                }
                return ranchers;
            }
        }

        public bool DeleteRancher(int id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteRancher";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    connection.Open();
                    object rowsAffected = command.ExecuteScalar();
                    connection.Close();
                    return (int.Parse(rowsAffected.ToString()) == 1);
                }
            }
        }
    }
}
