using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Migration.OldSystem
{
    public static class SupplierOld
    {
        private static string connectionString = "Data Source=75.126.164.59;Initial Catalog=garibay_iprojal;Persist Security Info=True;User ID=iprojal;Password=hg4274yr;Max Pool Size=400;";
        public static List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Proveedores"))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Supplier supplier = new Supplier();
                            supplier.Name = reader["Nombre"].ToString();
                            supplier.Address = reader["Direccion"].ToString();
                            supplier.ZipCode = reader["CP"].ToString();
                            supplier.CityOrDistrict = reader["Comunidad"].ToString() + reader["Municipio"].ToString();
                            supplier.StateId = int.Parse(reader["estadoID"].ToString());
                            supplier.PhoneNumber  = reader["Teléfono"].ToString();
                            supplier.MobilePhoneNumber = reader["Celular"].ToString();
                            supplier.Contact = reader["Nombrecontacto"].ToString();
                            supplier.BankData = reader["banco"].ToString();
                            supplier.Notes = reader["Observaciones"].ToString();
                            suppliers.Add(supplier);
                        }
                    }
                }
            }
            return suppliers;            
        }
    }
}
