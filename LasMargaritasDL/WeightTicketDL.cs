using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace LasMargaritas.DL
{
    public class WeightTicketsDL
    {
        public string ConnectionString { get; set; }

        public WeightTicketsDL(string connectionString)
        {
            ConnectionString = connectionString;
            excludedPropertiesInInsert = new List<string>();
            excludedPropertiesInUpdate = new List<string>();
            excludedPropertiesInReader = new List<string>();
            //excluding these while inserting
            excludedPropertiesInInsert.Add("Id");
            excludedPropertiesInInsert.Add("StoreTs");
            excludedPropertiesInInsert.Add("UpdateTs");
            excludedPropertiesInInsert.Add("IsEntranceWeightTicket");
            excludedPropertiesInInsert.Add("FirstPartPrinted");
            //exluding these while updating
            excludedPropertiesInUpdate.Add("StoreTs");
            excludedPropertiesInUpdate.Add("UpdateTs");
            excludedPropertiesInUpdate.Add("IsEntranceWeightTicket");
            excludedPropertiesInUpdate.Add("FirstPartPrinted");
            //exclude properties in reader
            excludedPropertiesInReader.Add("IsEntranceWeightTicket");

        }

        private List<string> excludedPropertiesInInsert;

        private List<string> excludedPropertiesInUpdate;

        private List<string> excludedPropertiesInReader;

        public List<SelectableModel> GetSelectableModels(int? cicleId)
        {
            List<SelectableModel> tickets = new List<SelectableModel>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetWeightTicketSelectableModels";
                    command.Parameters.Add("@CicleId", SqlDbType.Int).Value = cicleId;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        SelectableModel ticket = new SelectableModel();
                        ticket.Id = int.Parse(reader["Id"].ToString());
                        ticket.Name = reader["Folio"].ToString();
                        tickets.Add(ticket);
                    }
                    
                    connection.Close();
                }
                return tickets;
            }
        }

        public DataTable GetWeightTicketsReport(int? cicleId, int? productId, int? producerId, int? saleCustomerId, int? rancherId, int? supplierId, WeightTicketType? type,  bool? entranceWeightTicketsOnly, bool? exitWeightTicketsOnly)
        {
            DataTable dataTable = new DataTable();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetWeightTicketReport";
                    command.Parameters.Add("@productId", SqlDbType.Int).Value = productId.HasValue ? (object)productId : (object)DBNull.Value;
                    command.Parameters.Add("@cicleId", SqlDbType.Int).Value = cicleId.HasValue ? (object)cicleId : (object)DBNull.Value;
                    command.Parameters.Add("@producerId", SqlDbType.Int).Value = producerId.HasValue ? (object)producerId : (object)DBNull.Value;
                    command.Parameters.Add("@saleCustomerId", SqlDbType.Int).Value = saleCustomerId.HasValue ? (object)saleCustomerId : (object)DBNull.Value;
                    command.Parameters.Add("@rancherId", SqlDbType.Int).Value = rancherId.HasValue ? (object)rancherId : (object)DBNull.Value;
                    command.Parameters.Add("@supplierId", SqlDbType.Int).Value = supplierId.HasValue ? (object)supplierId : (object)DBNull.Value;
                    command.Parameters.Add("@exitWeightTicketsOnly", SqlDbType.Int).Value = exitWeightTicketsOnly.HasValue ? (object)exitWeightTicketsOnly : (object)DBNull.Value;
                    command.Parameters.Add("@entranceWeightTicketsOnly", SqlDbType.Int).Value = entranceWeightTicketsOnly.HasValue ? (object)entranceWeightTicketsOnly : (object)DBNull.Value;
                    command.Parameters.Add("@weightTicketType", SqlDbType.Int).Value = type.HasValue ? (object)type : (object)DBNull.Value;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.Fill(dataTable);
                    }
                }
                return dataTable;
            }
        }
        public WeightTicket InsertWeightTicket(WeightTicket weightTicket)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertWeightTicket";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in weightTicket.GetType().GetProperties() where !excludedPropertiesInInsert.Contains(x.Name) select x).ToArray())
                    {
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            command.Parameters.AddWithValue("@" + prop.Name, ((DateTime)prop.GetValue(weightTicket)).ToUniversalTime());
                        }
                        else if (prop.PropertyType == typeof(DateTime?) && ((DateTime?)prop.GetValue(weightTicket)).HasValue)
                        {
                            command.Parameters.AddWithValue("@" + prop.Name, ((DateTime)prop.GetValue(weightTicket)).ToUniversalTime());
                        }
                        else
                            command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(weightTicket));
                    }                    
                    connection.Open();
                    object weightTicketId = command.ExecuteScalar();
                    weightTicket.Id = int.Parse(weightTicketId.ToString());
                    connection.Close();
                }
                return weightTicket;
            }
        }

        public WeightTicket UpdateWeightTicket(WeightTicket weightTicket)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertWeightTicket";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in weightTicket.GetType().GetProperties() where !excludedPropertiesInUpdate.Contains(x.Name) select x).ToArray())
                    {
                        if(prop.PropertyType == typeof(DateTime))
                        {
                            command.Parameters.AddWithValue("@" + prop.Name,((DateTime)prop.GetValue(weightTicket)).ToUniversalTime());
                        }
                        else if (prop.PropertyType == typeof(DateTime?) && ((DateTime?)prop.GetValue(weightTicket)).HasValue)
                        {
                            command.Parameters.AddWithValue("@" + prop.Name, ((DateTime)prop.GetValue(weightTicket)).ToUniversalTime());
                        }
                        else
                            command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(weightTicket));
                    }
                    connection.Open();
                    object weightTicketId = command.ExecuteScalar();
                    weightTicket.Id = int.Parse(weightTicketId.ToString());
                    connection.Close();
                }
                return weightTicket;
            }
        }

        public List<WeightTicket> GetWeightTicket(int? id = null)
        {
            List<WeightTicket> tickets = new List<WeightTicket>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetWeightTicket";
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    tickets = DataReaderMapper.Map<WeightTicket>(reader, excludedPropertiesInReader);   
                    reader.Close();
                    connection.Close();
                }
                return tickets;
            }
        }

        public bool DeleteWeightTicket(int id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteWeightTicket";
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

