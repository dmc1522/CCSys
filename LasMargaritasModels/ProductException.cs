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
    }
}
