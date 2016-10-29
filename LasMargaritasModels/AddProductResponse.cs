namespace LasMargaritas.Models
{
    public class AddProductResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public Product Product { get; set; }
    }
}