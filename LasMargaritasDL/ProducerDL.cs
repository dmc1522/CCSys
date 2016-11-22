using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;

namespace LasMargaritas.DL
{
    public class ProducerDL
    {
        public string ConnectionString { get; set; }

        
        public ProducerDL(string connectionString)
        {
            ConnectionString = connectionString;
            excludedPropertiesInInsert = new List<string>();
            excludedPropertiesInUpdate = new List<string>();
         
            //excluding these while inserting
            excludedPropertiesInInsert.Add("Id");
            excludedPropertiesInInsert.Add("StoreTs");
            excludedPropertiesInInsert.Add("UpdateTs");
            //exluding these while updating
            excludedPropertiesInUpdate.Add("StoreTs");
            excludedPropertiesInUpdate.Add("UpdateTs");
            //excludedPropertiesInUpdate.Add("Photo");
        }

        private List<string> excludedPropertiesInInsert;

        private List<string> excludedPropertiesInUpdate;

        public Producer InsertProducer(Producer producer)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertProducer";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in producer.GetType().GetProperties() where !excludedPropertiesInInsert.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(producer));
                    }
                    /* if (producer.Photo != null)
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            producer.Photo.Save(stream, producer.Photo.RawFormat);
                            command.Parameters.Add(new SqlParameter("@Photo", SqlDbType.Image)).Value = stream.GetBuffer();
                        }
                    }*/
                    connection.Open();
                    object producerId = command.ExecuteScalar();
                    producer.Id = int.Parse(producerId.ToString());
                    connection.Close();
                }
                return producer;
            }
        }

        public Producer UpdateProducer(Producer producer)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertProducer";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in producer.GetType().GetProperties() where !excludedPropertiesInUpdate.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(producer));
                    }
                    connection.Open();
                    object producerId = command.ExecuteScalar();
                    producer.Id = int.Parse(producerId.ToString());
                    connection.Close();
                }
                return producer;
            }
        }

        public List<Producer> GetProducer(int? id = null)
        {
            List<Producer> producers = new List<Producer>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetProducer";
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    producers = DataReaderMapper.Map<Producer>(reader);
                    reader.Close();
                    connection.Close();
                }
                return producers;
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
                    command.CommandText = "spGetProducerSelectableModels";
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

        public bool DeleteProducer(int id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteProducer";
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
