using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace LasMargaritas.DL
{
    public class WareHouseDL
    {
        public string ConnectionString { get; set; }
        public WareHouseDL(string connectionString)
        {
            ConnectionString = connectionString;
            excludedPropertiesInInsert = new List<string>();
            excludedPropertiesInUpdate = new List<string>();

            //excluding these while inserting
            excludedPropertiesInInsert.Add("Id");
        }

        private List<string> excludedPropertiesInInsert;

        private List<string> excludedPropertiesInUpdate;

        public WareHouse InsertWareHouse(WareHouse wareHouse)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertWareHouse";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in wareHouse.GetType().GetProperties() where !excludedPropertiesInInsert.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(wareHouse));
                    }
                    connection.Open();
                    object wareHouseId = command.ExecuteScalar();
                    wareHouse.Id = int.Parse(wareHouseId.ToString());
                    connection.Close();
                }
                return wareHouse;
            }
        }
        public List<SelectableModel> GetBasicModels()
        {
            List<SelectableModel> producers = new List<SelectableModel>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetWareHouseSelectableModels";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    producers = DataReaderMapper.Map<SelectableModel>(reader);
                    reader.Close();
                    connection.Close();
                }
                return producers;
            }
        }
        public WareHouse UpdateWareHouse(WareHouse wareHouse)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertWareHouse";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in wareHouse.GetType().GetProperties() where !excludedPropertiesInUpdate.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(wareHouse));
                    }
                    connection.Open();
                    object wareHouseId = command.ExecuteScalar();
                    wareHouse.Id = int.Parse(wareHouseId.ToString());
                    connection.Close();
                }
                return wareHouse;
            }
        }

        public List<WareHouse> GetWareHouse(int? id = null)
        {
            List<WareHouse> wareHouses = new List<WareHouse>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spWareHouse";
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    wareHouses = DataReaderMapper.Map<WareHouse>(reader);
                    reader.Close();
                    connection.Close();
                }
                return wareHouses;
            }
        }

        public bool DeleteWareHouse(int id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteWareHouse";
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
