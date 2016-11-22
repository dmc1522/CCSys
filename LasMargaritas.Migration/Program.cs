using LasMargaritas.BL;
using LasMargaritas.Migration.OldSystem;
using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Migration
{
    class Program
    {
        static string connectionString = string.Empty;
        static void Main(string[] args)
        {
            bool production = false;
            if(production)
                connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            else
                connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //MigrateProducts();
            //MigrateProducers();
            MigrateSaleCustomers();
            MigrateRanchers();
            MigrateSuppliers();
            
        }
    
        private static void MigrateProducts()
        {
            List<string> exceptions = new List<string>();
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDevDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            List<Product> products = ProductOld.GetProducts();
            ProductBL bl = new ProductBL(connectionString);
            foreach (Product product in products)
            {
                try
                {                   
                    bl.InsertProduct(product);
                }
                catch (Exception ex)
                {
                    exceptions.Add(product.Name + ". Error " + ex.Message);
                }
            }
            foreach (string ex in exceptions)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static void MigrateProducers()
        {
            List<string> exceptions = new List<string>();
            List<Producer> producers = ProducerOld.GetProducers();
            ProducerBL bl = new ProducerBL(connectionString);
            foreach (Producer producer in producers)
            {
                try
                {
                    string[] parts = producer.LastName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Count() == 3)
                    {
                        producer.Name = parts[2];
                        producer.LastName = parts[0] + " " + parts[1];
                    }
                    else if (parts.Count() == 2)
                    {
                        producer.Name = parts[1];
                        producer.LastName = parts[0];
                    }
                    else
                    {
                        producer.Name = producer.LastName;
                        producer.LastName = string.Empty;
                    }
                    bl.InsertProducer(producer);
                }
                catch (Exception ex)
                {
                    exceptions.Add(producer.Name + " " + producer.LastName + ". Error " + ex.Message);
                }
            }
            foreach (string ex in exceptions)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static void MigrateSaleCustomers()
        {
            List<string> exceptions = new List<string>();
            List<SaleCustomer> customers = SaleCustomerOld.GetSaleCustomer();
            SaleCustomerBL customerBL = new SaleCustomerBL(connectionString);
            foreach (SaleCustomer customer in customers)
            {
                try
                {                    
                    customerBL.InsertSaleCustomer(customer);
                }
                catch (Exception ex)
                {
                    exceptions.Add(customer.Name + ". Error " + ex.Message);
                }
            }
            foreach (string ex in exceptions)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static void MigrateRanchers()
        {
            List<string> exceptions = new List<string>();
            List<Rancher> ranchers = RancherOld.GetRanchers();
            RancherBL rancherBL = new RancherBL(connectionString);
            foreach (Rancher rancher in ranchers)
            {
                try
                {
                    rancherBL.InsertRancher(rancher);
                }
                catch (Exception ex)
                {
                    exceptions.Add(rancher.Name + ". Error " + ex.Message);
                }
            }
            foreach (string ex in exceptions)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static void MigrateSuppliers()
        {
            List<string> exceptions = new List<string>();
            List<Supplier> suppliers = SupplierOld.GetSuppliers();
            SupplierBL supplierBL = new SupplierBL(connectionString);
            foreach (Supplier supplier in suppliers)
            {
                try
                {
                    supplierBL.InsertSupplier(supplier);
                }
                catch (Exception ex)
                {
                    exceptions.Add(supplier.Name + ". Error " + ex.Message);
                }
            }
            foreach (string ex in exceptions)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }
}
