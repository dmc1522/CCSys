using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Migration.OldSystem
{
    public static class RancherOld
    {
        private static string connectionString = "Data Source=75.126.164.59;Initial Catalog=garibay_iprojal;Persist Security Info=True;User ID=iprojal;Password=hg4274yr;Max Pool Size=400;";
        public static List<Rancher> GetRanchers()
        {
            List<Rancher> ranchers = new List<Rancher>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM gan_Proveedores"))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Rancher rancher = new Rancher();
                            rancher.Name = reader["Nombre"].ToString();
                            rancher.Address = reader["direccion"].ToString();
                            rancher.City = reader["ciudad"].ToString();
                            rancher.StateId = int.Parse(reader["estadoID"].ToString());
                            rancher.RFC  = reader["RFC"].ToString();
                            ranchers.Add(rancher);
                        }
                    }
                }
            }
            return ranchers;            
        }
    }
}
