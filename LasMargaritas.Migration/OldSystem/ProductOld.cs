using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Migration.OldSystem
{
    public static class ProductOld
    {
        private static string connectionString = "Data Source=75.126.164.59;Initial Catalog=garibay_iprojal;Persist Security Info=True;User ID=iprojal;Password=hg4274yr;Max Pool Size=400;";
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("select * from productos inner join presentaciones on productos.presentacionId = presentaciones.presentacionId inner join unidades on productos.unidadId = unidades.unidadId inner join productoGrupos on productos.productoGrupoId = productoGrupos.grupoId left join casaagricola on productos.casaagricolaId = casaagricola.casaagricolaId"))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Product product = new Product();
                            product.Name = reader["Nombre"].ToString();
                            product.Code = reader["codigo"].ToString();
                            product.Description = reader["descripcion"].ToString();
                            product.Price1 = float.Parse(reader["precio1"].ToString());
                            product.Price2 = float.Parse(reader["precio2"].ToString());
                            product.Price3 = float.Parse(reader["precio3"].ToString());
                            product.Price4 = float.Parse(reader["precio4"].ToString());
                            product.ScaleCode = reader["codigoBascula"].ToString();
                            if(reader["unidadID"] != null)
                            {
                                product.UnitId = ProductHelperNew.GetUnit(reader["Unidad"].ToString());
                            }
                            if (reader["presentacionID"] != null)
                            {
                                product.PresentationId = ProductHelperNew.GetPresentation(reader["Presentacion"].ToString());
                            }
                            if (reader["productoGrupoId"] != null)
                            {
                                product.ProductGroupId = ProductHelperNew.GetProductGroup(reader["grupo"].ToString());
                            }
                            if (reader["CasaAgricola"] != null)
                            {
                                product.AgriculturalBrandId = ProductHelperNew.GetAgriculturalBrand(reader["CasaAgricola"].ToString());
                            }                         
                            products.Add(product);                            
                        }
                    }
                }
            }
            return products;            
        }
    }
}
