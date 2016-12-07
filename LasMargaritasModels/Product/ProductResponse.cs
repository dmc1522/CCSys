namespace LasMargaritas.Models
{
    public class ProductResponse
    {
      
        public ProductError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public Product Product { get; set; }
    }
}