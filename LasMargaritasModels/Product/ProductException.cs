using System;

namespace LasMargaritas.Models
{
    public class ProductException: Exception
    {
        public ProductError Error { get; set; }

        public ProductException(ProductError error)
        {
            Error = error;
        }
        public ProductException(ProductError error, string message):base(message)
        {
            Error = error;
        }
        public ProductException(string message) : base(message)
        {

        }
    }
}
