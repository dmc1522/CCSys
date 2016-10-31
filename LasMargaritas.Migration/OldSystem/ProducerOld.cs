using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Migration.OldSystem
{
    public static class ProducerOld
    {
        private static string connectionString = "Data Source=75.126.164.59;Initial Catalog=garibay_iprojal;Persist Security Info=True;User ID=iprojal;Password=hg4274yr;Max Pool Size=400;";
        public static List<Producer> GetProducers()
        {
            List<Producer> producers = new List<Producer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Productores where apaterno = 'RANCHO EL AHUILOTE' "))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Producer producer = new Producer();
                            producer.LastName = reader["apaterno"].ToString() + " " + reader["amaterno"].ToString();
                            producer.Name = reader["Nombre"].ToString();
                            producer.BirthDate = reader["fechanacimiento"] == DBNull.Value ? (DateTime?)null : DateTime.Parse(reader["fechanacimiento"].ToString());
                            producer.INE = reader["IFE"].ToString();
                            producer.CURP = reader["CURP"].ToString();
                            producer.Address = reader["domicilio"].ToString();
                            producer.City = reader["poblacion"].ToString();
                            producer.County = reader["municipio"].ToString();
                            producer.StateId = int.Parse(reader["estadoID"].ToString());
                            producer.ZipCode = reader["CP"].ToString();
                            producer.RFC = reader["RFC"].ToString();
                            producer.SexId = int.Parse(reader["sexoID"].ToString());
                            producer.PhoneNumber = reader["telefono"].ToString();
                            producer.WorkPhoneNumber = reader["telefonotrabajo"].ToString();
                            producer.MobilePhoneNumber = reader["celular"].ToString();
                            producer.Fax = reader["fax"].ToString();
                            producer.Email = reader["email"].ToString();
                            producer.CivilStatusId = reader["estadocivilID"] == DBNull.Value ?1 :  int.Parse(reader["estadocivilID"].ToString());
                            producer.RegimeId = reader["regimenId"] == DBNull.Value ? 0 :  int.Parse(reader["regimenId"].ToString());
                            producer.RegimeId += 1; //mismatch in catalog
                            producer.DistrinctOrColony = reader["colonia"].ToString();
                            producer.Spouse = reader["conyugue"].ToString();
                            producers.Add(producer);
                        }
                    }
                }
            }
            return producers;            
        }
    }
}
