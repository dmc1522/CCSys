using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.ObjectModel;

namespace LasMargaritas.DL
{
    public class SettlementDL
    {
        #region Public properties
        public string ConnectionString { get; set; }
        #endregion

        #region Private properties
        private List<string> excludedPropertiesInInsert;

        private List<string> excludedPropertiesInUpdate;

        private List<string> excludedPropertiesInReader;
        #endregion

        #region Constructor
        public SettlementDL(string connectionString)
        {
            ConnectionString = connectionString;
            excludedPropertiesInInsert = new List<string>();
            excludedPropertiesInUpdate = new List<string>();
            excludedPropertiesInReader = new List<string>();
            //excluding these while reading
            excludedPropertiesInReader.Add("WeightTicketsIds");
            excludedPropertiesInReader.Add("TypeString");
            //excluding these while inserting
            excludedPropertiesInInsert.Add("Id");
            excludedPropertiesInInsert.Add("StoreTs");
            excludedPropertiesInInsert.Add("UpdateTs");
            excludedPropertiesInInsert.Add("ProductName");
            excludedPropertiesInInsert.Add("WeightTicketsIds");
            excludedPropertiesInInsert.Add("ProducerName");
            excludedPropertiesInInsert.Add("TypeString");
            //exluding these while updating
            excludedPropertiesInUpdate.Add("StoreTs");
            excludedPropertiesInUpdate.Add("UpdateTs");
            excludedPropertiesInUpdate.Add("ProductName");
            excludedPropertiesInUpdate.Add("WeightTicketsIds");
            excludedPropertiesInUpdate.Add("ProducerName");
            excludedPropertiesInUpdate.Add("TypeString");
        }
        #endregion

        #region SettlementPayments
        public bool DeleteSettlementPayment(int paymentId)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteSettlementPayment";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = paymentId;
                    connection.Open();
                    object rowsAffected = command.ExecuteScalar();
                    connection.Close();
                    return (int.Parse(rowsAffected.ToString()) == 1);
                }

            }
        }
        public List<SettlementPayment> GetSettlementPayments(int settlementId)
        {
            List<SettlementPayment> payments = new List<SettlementPayment>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetSettlementPayments";
                    command.Parameters.Add("@SettlementId", SqlDbType.Int).Value = settlementId;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    payments = DataReaderMapper.Map<SettlementPayment>(reader, excludedPropertiesInReader);
                    reader.Close();                    
                    connection.Close();
                }
                return payments;
            }

        }
        public List<SelectableModel> GetSettlementSelectableModels(int cicleId)
        {
            List<SelectableModel> settlements = new List<SelectableModel>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetSettlementSelectableModels";
                    command.Parameters.Add("@CicleId", SqlDbType.Int).Value = cicleId;                    
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        SelectableModel settlement = new SelectableModel();
                        settlement.Id = int.Parse(reader["Id"].ToString());
                        settlement.Name = reader["Producer"].ToString();
                        settlements.Add(settlement);
                    }                    
                    connection.Close();
                }
                return settlements;
            }
        }

        public SettlementPayment AddSettlementPayment(SettlementPayment payment)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertSettlementPayment";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in payment.GetType().GetProperties() where !excludedPropertiesInInsert.Contains(x.Name) select x).ToArray())
                    {
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            command.Parameters.AddWithValue("@" + prop.Name, ((DateTime)prop.GetValue(payment)).ToUniversalTime());
                        }
                        else if (prop.PropertyType == typeof(DateTime?) && ((DateTime?)prop.GetValue(payment)).HasValue)
                        {
                            command.Parameters.AddWithValue("@" + prop.Name, ((DateTime)prop.GetValue(payment)).ToUniversalTime());
                        }
                        else
                            command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(payment));
                    }
                    connection.Open();
                    object settlementId = command.ExecuteScalar();
                    payment.Id = int.Parse(settlementId.ToString());
                    connection.Close();
                }
                return payment;
            }
        }

        public bool DeleteAllSettlementPayments(int settlementId)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteAllSettlementPayments";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@SettlementId", SqlDbType.Int).Value = settlementId;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
            }
        }
        #endregion

        #region Settlement
        public Settlement InsertSettlement(Settlement settlement)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertSettlement";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in settlement.GetType().GetProperties() where !excludedPropertiesInInsert.Contains(x.Name) select x).ToArray())
                    {
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            command.Parameters.AddWithValue("@" + prop.Name, ((DateTime)prop.GetValue(settlement)).ToUniversalTime());
                        }
                        else if (prop.PropertyType == typeof(DateTime?) && ((DateTime?)prop.GetValue(settlement)).HasValue)
                        {
                            command.Parameters.AddWithValue("@" + prop.Name, ((DateTime)prop.GetValue(settlement)).ToUniversalTime());
                        }
                        else
                            command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(settlement));
                    }                    
                    connection.Open();
                    object settlementId = command.ExecuteScalar();
                    settlement.Id = int.Parse(settlementId.ToString());
                    connection.Close();
                }
                return settlement;
            }
        }

        public Settlement UpdateSettlement(Settlement settlement)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spUpsertSettlement";
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (PropertyInfo prop in (from x in settlement.GetType().GetProperties() where !excludedPropertiesInUpdate.Contains(x.Name) select x).ToArray())
                    {
                        if(prop.PropertyType == typeof(DateTime))
                        {
                            command.Parameters.AddWithValue("@" + prop.Name,((DateTime)prop.GetValue(settlement)).ToUniversalTime());
                        }
                        else if (prop.PropertyType == typeof(DateTime?) && ((DateTime?)prop.GetValue(settlement)).HasValue)
                        {
                            command.Parameters.AddWithValue("@" + prop.Name, ((DateTime)prop.GetValue(settlement)).ToUniversalTime());
                        }
                        else
                            command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(settlement));
                    }
                    connection.Open();
                    object settlementId = command.ExecuteScalar();
                    settlement.Id = int.Parse(settlementId.ToString());
                    connection.Close();
                }
                return settlement;
            }
        }

        public List<Settlement> GetSettlement(int? id = null, int? cicleId = null)
        {
            List<Settlement> settlements = new List<Settlement>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetSettlement";
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@CicleId", SqlDbType.Int).Value = cicleId;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    settlements = DataReaderMapper.Map<Settlement>(reader, excludedPropertiesInReader);   
                    reader.Close();
                    connection.Close();
                }
                return settlements;
            }
        }

        public bool DeleteSettlement(int id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteSettlement";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    connection.Open();
                    object rowsAffected = command.ExecuteScalar();
                    connection.Close();
                    return (int.Parse(rowsAffected.ToString()) == 1);                 
                }
              
            }
        }

        public bool DeleteAllWeightTicketsFromSettlement(int id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spDeleteAllWeightTicketsFromSettlement";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }

            }
        }
        
        public bool AddWeightTicketToSettlement(int settlementId, int weighTicketId)
        {            
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spAddWeightTicketToSettlement";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SettlementId", SqlDbType.Int)).Value = settlementId;
                    command.Parameters.Add(new SqlParameter("@WeightTicketId", SqlDbType.Int)).Value = weighTicketId;
                    connection.Open();
                    object rowsAffected = command.ExecuteScalar();
                    connection.Close();
                    return (int.Parse(rowsAffected.ToString()) == 1);
                }               
            }
        }
        
        public Collection<int> GetWeightTicketsInSettlement(int id)
        {
            Collection<int> settlements = new Collection<int>();
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    command.Connection = connection;
                    command.CommandText = "spGetWeightTicketsInSettlement";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        settlements.Add(int.Parse(reader["WeightTicketId"].ToString()));
                    }
                    reader.Close();
                    connection.Close();
                }

            }
            return settlements;
        }
        #endregion
    }
}

