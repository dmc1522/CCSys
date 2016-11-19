namespace LasMargaritas.Models
{
    public class SupplierResponse
    {
        public SupplierError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public Supplier Supplier { get; set; }
    }
}