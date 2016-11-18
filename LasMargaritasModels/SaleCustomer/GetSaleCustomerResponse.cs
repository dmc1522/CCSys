using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetSaleCustomerResponse
    {
        public GetSaleCustomerResponse()
        {
            SaleCustomers = new List<SaleCustomer>();
        }
        public SaleCustomerError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<SaleCustomer> SaleCustomers { get; set; }
    }
}
