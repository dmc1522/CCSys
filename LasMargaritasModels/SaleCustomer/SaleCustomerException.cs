using System;

namespace LasMargaritas.Models
{
    public class SaleCustomerException : Exception
    {
        public SaleCustomerError Error { get; set; }

        public SaleCustomerException(SaleCustomerError error)
        {
            Error = error;
        }

        public SaleCustomerException(SaleCustomerError error, string message): base(message)
        {
            Error = error;
        }

        public SaleCustomerException(string message) : base(message)
        {

        }
    }
}
