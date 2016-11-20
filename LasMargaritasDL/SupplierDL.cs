using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace LasMargaritas.DL
{
    public class SupplierDL
    {
        public string ConnectionString { get; set; }
        public SupplierDL(string connectionString)
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

        public Supplier InsertSupplier(Supplier supplier)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertSupplier";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in supplier.GetType().GetProperties() where !excludedPropertiesInInsert.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(supplier));
                    }
                    connection.Open();
                    object supplierId = command.ExecuteScalar();
                    supplier.Id = int.Parse(supplierId.ToString());
                    connection.Close();
                }
                return supplier;
            }
        }

        public Supplier UpdateSupplier(Supplier supplier)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertSupplier";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in supplier.GetType().GetProperties() where !excludedPropertiesInUpdate.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(supplier));
                    }
                    connection.Open();
                    object supplierId = command.ExecuteScalar();
                    supplier.Id = int.Parse(supplierId.ToString());
                    connection.Close();
                }
                return supplier;
            }
        }

        public List<Supplier> GetSupplier(int? id = null)
        {
            List<Supplier> producers = new List<Supplier>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetSupplier";
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    producers = DataReaderMapper.Map<Supplier>(reader);
                    reader.Close();
                    connection.Close();
                }
                return producers;
            }
        }

        public List<SelectableModel> GetBasicModels()
        {
            List<SelectableModel> suppliers = new List<SelectableModel>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetSupplierSelectableModels";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    suppliers = DataReaderMapper.Map<SelectableModel>(reader);
                    reader.Close();
                    connection.Close();
                }
                return suppliers;
            }
        }

        public bool DeleteSupplier(int id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteSupplier";
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
