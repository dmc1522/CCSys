﻿using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace LasMargaritas.DL
{
    public class SaleCustomerDL
    {
        public string ConnectionString { get; set; }
        public SaleCustomerDL(string connectionString)
        {
            ConnectionString = connectionString;
            excludedPropertiesInInsert = new List<string>();
            excludedPropertiesInUpdate = new List<string>();

            //excluding these while inserting
            excludedPropertiesInInsert.Add("Id");
            excludedPropertiesInInsert.Add("StoreTS");
            excludedPropertiesInInsert.Add("UpdateTS");


            //exluding these while updating
            excludedPropertiesInUpdate.Add("StoreTS");
            excludedPropertiesInUpdate.Add("UpdateTS");
        }

        private List<string> excludedPropertiesInInsert;

        private List<string> excludedPropertiesInUpdate;

        public SaleCustomer InsertSaleCustomer(SaleCustomer saleCustomer)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertSaleCustomer";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in saleCustomer.GetType().GetProperties() where !excludedPropertiesInInsert.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(saleCustomer));
                    }
                    connection.Open();
                    object saleCustomerId = command.ExecuteScalar();
                    saleCustomer.Id = int.Parse(saleCustomerId.ToString());
                    connection.Close();
                }
                return saleCustomer;
            }
        }

        public SaleCustomer UpdateSaleCustomer(SaleCustomer saleCustomer)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertSaleCustomer";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in saleCustomer.GetType().GetProperties() where !excludedPropertiesInUpdate.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(saleCustomer));
                    }
                    connection.Open();
                    object saleCustomerId = command.ExecuteScalar();
                    saleCustomer.Id = int.Parse(saleCustomerId.ToString());
                    connection.Close();
                }
                return saleCustomer;
            }
        }

        public List<SaleCustomer> GetSaleCustomer(int? id = null)
        {
            List<SaleCustomer> producers = new List<SaleCustomer>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetSaleCustomer";
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    producers = DataReaderMapper.Map<SaleCustomer>(reader);
                    reader.Close();
                    connection.Close();
                }
                return producers;
            }
        }

        public bool DeleteSaleCustomer(int id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteSaleCustomer";
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
