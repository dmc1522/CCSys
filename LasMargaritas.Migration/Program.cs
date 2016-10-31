using LasMargaritas.BL;
using LasMargaritas.Migration.OldSystem;
using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            MigrateProducts();

        }
        private static void MigrateProducts()
        {
            List<string> exceptions = new List<string>();
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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
    }
}
