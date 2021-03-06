﻿using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace LasMargaritas.DL
{
    public class ProductDL
    {
        public string ConnectionString { get; set; }

     
        public ProductDL(string connectionString)
        {
            ConnectionString = connectionString;
            excludedPropertiesInInsert = new List<string>();
            excludedPropertiesInUpdate = new List<string>();
            //excluding these while inserting
            excludedPropertiesInInsert.Add("Id");
            excludedPropertiesInInsert.Add("StoreTs");
            excludedPropertiesInInsert.Add("UpdateTs");
            //exluding these while updating
            excludedPropertiesInUpdate.Add("StoreTs");
            excludedPropertiesInUpdate.Add("UpdateTs");
        }
        private List<string> excludedPropertiesInInsert;

        private List<string> excludedPropertiesInUpdate;
        public Product InsertProduct(Product product)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertProduct";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in product.GetType().GetProperties() where !excludedPropertiesInInsert.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(product));
                    }
                    connection.Open();
                    object productId = command.ExecuteScalar();
                    product.Id = int.Parse(productId.ToString());
                    connection.Close();
                }
                return product;
            }
        }

          public List<SelectableModel> GetBasicModels()
        {
            List<SelectableModel> products = new List<SelectableModel>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetProductSelectableModels";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    products = DataReaderMapper.Map<SelectableModel>(reader);
                    reader.Close();
                    connection.Close();
                }
                return products;
            }
        }

        public Product UpdateProduct(Product product)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertProduct";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in product.GetType().GetProperties() where !excludedPropertiesInUpdate.Contains(x.Name) select x).ToArray())
                    {
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(product));
                    }
                    connection.Open();
                    object productId = command.ExecuteScalar();
                    product.Id = int.Parse(productId.ToString());
                    connection.Close();
                }
                return product;
            }
        }

        public List<Product> GetProduct(int? id = null)
        {
            List<Product> products = new List<Product>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetProduct";
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    products = DataReaderMapper.Map<Product>(reader);
                    reader.Close();
                    connection.Close();
                }
                return products;
            }
        }

        public List<SelectableModel> GetProductByProductGroupId(WeightTicketType type)
        {
            List<SelectableModel> products = new List<SelectableModel>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetWeightTicketProducts";
                    command.Parameters.Add("@WeightTicketType", SqlDbType.Int).Value = (int)type;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    products = DataReaderMapper.Map<SelectableModel>(reader);
                    reader.Close();
                    connection.Close();
                }
                return products;
            }
        }

        public bool DeleteProduct(int id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteProduct";
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
