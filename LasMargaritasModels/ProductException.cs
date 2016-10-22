using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
