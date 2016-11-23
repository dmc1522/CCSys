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

