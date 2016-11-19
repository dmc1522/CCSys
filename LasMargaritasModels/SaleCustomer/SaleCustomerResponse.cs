namespace LasMargaritas.Models
{
    public class SaleCustomerResponse
    {
        public SaleCustomerError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public SaleCustomer SaleCustomer { get; set; }
    }
}