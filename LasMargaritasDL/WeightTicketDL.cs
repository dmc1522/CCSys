using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
namespace LasMargaritas.DL
{
    public class WeightTicketsDL
    {
        public string ConnectionString { get; set; }

        public WeightTicketsDL(string connectionString)
        {
            ConnectionString = connectionString;
            excludedPropertiesInInsert = new List<string>();
            excludedPropertiesInInsert.Add("Id");
            excludedPropertiesInInsert.Add("StoreTs");
            excludedPropertiesInInsert.Add("UpdateTs");
        }

        private List<string> excludedPropertiesInInsert;

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
                    tickets = DataReaderMapper.Map<WeightTicket>(reader);   
                    reader.Close();
                    connection.Close();
                }
                return tickets;
            }
        }
    }
}
