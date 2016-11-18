using System;

namespace LasMargaritas.Models
{
    public class SupplierException : Exception
    {
        public SupplierError Error { get; set; }

        public SupplierException(SupplierError error)
        {
            Error = error;
        }

        public SupplierException(SupplierError error, string message): base(message)
        {
            Error = error;
        }

        public SupplierException(string message) : base(message)
        {

        }
    }
}
