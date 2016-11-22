using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Migration.OldSystem
{
    public static class SaleCustomerOld
    {
        private static string connectionString = "Data Source=75.126.164.59;Initial Catalog=garibay_iprojal;Persist Security Info=True;User ID=iprojal;Password=hg4274yr;Max Pool Size=400;";
        public static List<SaleCustomer> GetSaleCustomer()
        {
            List<SaleCustomer> customers = new List<SaleCustomer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM ClientesVentas"))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            SaleCustomer customer = new SaleCustomer();
                            customer.Name = reader["nombre"].ToString();
                            customer.Address = reader["domicilio"].ToString();
                            customer.City = reader["ciudad"].ToString();
                            customer.PhoneNumber = reader["telefono"].ToString();
                            customer.StateId = int.Parse(reader["estadoID"].ToString());
                            customer.RFC = reader["RFC"].ToString();
                            customer.DistrictOrColony = reader["colonia"].ToString();
                            customer.ZipCode = reader["CP"].ToString();
                            customers.Add(customer);
                        }
                    }
                }
            }
            return customers;            
        }
    }
}
