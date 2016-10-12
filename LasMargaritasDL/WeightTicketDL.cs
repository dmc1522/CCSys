using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace LasMargaritas.DL
{
    public class WeightTicketsDL
    {
        public string ConnectionString { get; set; }

        public WeightTicketsDL(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public WeightTicket InsertWeightTicket(WeightTicket weightTicket)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spInsertWeightTicket";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Number", SqlDbType.VarChar).Value = weightTicket.Number;
                    command.Parameters.Add("@EntranceWeightKg", SqlDbType.Float).Value = weightTicket.EntranceWeightKg;
                    command.Parameters.Add("@ExitWeightKg", SqlDbType.Float).Value = weightTicket.ExitWeightKg;
                    command.Parameters.Add("@CicleId", SqlDbType.Int).Value = weightTicket.CicleId;
                    connection.Open();
                    object weightTicketId = command.ExecuteScalar();
                    weightTicket.Id = int.Parse(weightTicketId.ToString());
                    connection.Close();
                }
                return weightTicket;
            }
        }

        public List<WeightTicket> GetWeightTicket()
        {
            List<WeightTicket> tickets = new List<WeightTicket>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetWeightTicket";
                    //TODO ADD FILTERS
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        WeightTicket ticket = new WeightTicket();
                        ticket.Id = int.Parse(reader["Id"].ToString());
                        ticket.CicleId = int.Parse(reader["CicleId"].ToString());
                        ticket.EntranceWeightKg = float.Parse(reader["EntranceWeightKg"].ToString());
                        ticket.ExitWeightKg = float.Parse(reader["ExitWeightKg"].ToString());
                        ticket.Number = reader["Number"].ToString();
                        tickets.Add(ticket);                
                    }
                    reader.Close();
                    connection.Close();
                }
                return tickets;
            }
        }
    }
}
