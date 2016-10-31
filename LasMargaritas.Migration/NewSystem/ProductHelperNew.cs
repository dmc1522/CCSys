using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Migration.OldSystem
{
    public static class ProductHelperNew
    {
        private static string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static int GetUnit(string unit)
        {          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Id FROM Unit where Name = @Name"))
                {
                    command.Connection = connection;
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.VarChar)).Value = unit;
                    return int.Parse(command.ExecuteScalar().ToString());
                }
            }            
        }
        public static int GetPresentation(string presentation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Id FROM Presentation where Name = @Name"))
                {
                    command.Connection = connection;
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.VarChar)).Value = presentation;
                    return int.Parse(command.ExecuteScalar().ToString());
                }
            }
        }

        public static int GetProductGroup(string group)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Id FROM ProductGroup where Name = @Name"))
                {
                    command.Connection = connection;
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.VarChar)).Value = group;
                    return int.Parse(command.ExecuteScalar().ToString());
                }
            }
        }

        public static int GetAgriculturalBrand(string brand)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Id FROM AgriculturalBrand where Name = @Name"))
                {
                    command.Connection = connection;
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.VarChar)).Value = brand;
                    return int.Parse(command.ExecuteScalar().ToString());
                }
            }
        }
    }
}
