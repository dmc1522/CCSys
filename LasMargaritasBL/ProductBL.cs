using LasMargaritas.DL;
using LasMargaritas.Models;
using System.Collections.Generic;


namespace LasMargaritas.BL
{
    public class ProductBL
    {
        private ProductDL productDL;
        private const int WeightTicketProductGroupId = 3;
        public ProductBL(string connectionString)
        {
            productDL = new ProductDL(connectionString);
        }

        public Product InsertProduct(Product product)
        {
              //Add validations here!
            ProductError result = ProductError.None;
            if (string.IsNullOrEmpty(product.Name))
                result |= ProductError.InvalidName;
            if (product.UnitId<= 0)
                result |= ProductError.InvalidUnit;         
            if (product.ProductGroupId <= 0)
                result |= ProductError.InvalidProductGroup;
            if (product.AgriculturalBrandId <= 0)
                result |= ProductError.InvalidAgriculturalBrand;
            if (result != ProductError.None)
                throw new ProductException(result);
            else
                return productDL.InsertProduct(product);            
        }

        public Product UpdateProduct(Product product)
        {
            //Add validations here!
            ProductError result = ProductError.None;
            if (string.IsNullOrEmpty(product.Name))
                result |= ProductError.InvalidName;
            if (product.UnitId <= 0)
                result |= ProductError.InvalidUnit;
            if (string.IsNullOrEmpty(product.ScaleCode))
                result |= ProductError.InvalidScaleCode;
            if (product.ProductGroupId <= 0)
                result |= ProductError.InvalidProductGroup;
            if (product.AgriculturalBrandId <= 0)
                result |= ProductError.InvalidAgriculturalBrand;
            if (result != ProductError.None)
                throw new ProductException(result);
            else
                return productDL.UpdateProduct(product);
        }

        public List<Product> GetProduct(int? id = null)
        {
            //Add validations here!
            ProductError result = ProductError.None;
            if (id != null && id <= 0)
                result |= ProductError.InvalidId;

            if (result != ProductError.None)
                throw new ProductException(result);
            else
                return productDL.GetProduct(id);
        }

        public List<Product> GetWeightTicketProducts()
        {
            //Add validations here!           
            return productDL.GetProductByProductGroupId(WeightTicketProductGroupId);
        }

        public bool DeleteProduct(int id)
        {
            //Add validations here!
            ProductError result = ProductError.None;
            if (id <= 0)
                result |= ProductError.InvalidId;

            if (result != ProductError.None)
                throw new ProductException(result);
            else
                return productDL.DeleteProduct(id);
        }

    }
}
