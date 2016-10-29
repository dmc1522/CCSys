using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasMargaritas.Models
{
    public class AddProductResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public Product Product { get; set; }
    }
}