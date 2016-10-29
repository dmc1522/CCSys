using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetProductResponse
    {
        public GetProductResponse()
        {
            Products = new List<Product>();
        }
        public ProductError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<Product> Products { get; set; }
    }
}
