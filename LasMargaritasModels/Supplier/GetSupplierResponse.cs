using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetSupplierResponse
    {
        public GetSupplierResponse()
        {
            Suppliers = new List<Supplier>();
        }
        public SupplierError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<Supplier> Suppliers { get; set; }
    }
}
