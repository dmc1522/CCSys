using LasMargaritas.DL;
using LasMargaritas.Models;
using System.Collections.Generic;


namespace LasMargaritas.BL
{
    public class SupplierBL
    {
        private SupplierDL supplierDL;
        private const int WeightTicketSupplierGroupId = 3;
        public SupplierBL(string connectionString)
        {
            supplierDL = new SupplierDL(connectionString);
        }

        public Supplier InsertSupplier(Supplier customer)
        {
            //Add validations here!
            SupplierError result = SupplierError.None;
            if (string.IsNullOrEmpty(customer.Name))
                result |= SupplierError.InvalidName;
            if (customer.StateId <= 0)
                result |= SupplierError.InvalidState;
            if (result != SupplierError.None)
                throw new SupplierException(result);
            else
                return supplierDL.InsertSupplier(customer);
        }

        public Supplier UpdateSupplier(Supplier customer)
        {
            //Add validations here!
            SupplierError result = SupplierError.None;
            if (string.IsNullOrEmpty(customer.Name))
                result |= SupplierError.InvalidName;
            if (customer.StateId <= 0)
                result |= SupplierError.InvalidState;
            if (result != SupplierError.None)
                throw new SupplierException(result);
            else
                return supplierDL.UpdateSupplier(customer);
        }

        public List<Supplier> GetSupplier(int? id = null)
        {
            //Add validations here!
            SupplierError result = SupplierError.None;
            if (id != null && id <= 0)
                result |= SupplierError.InvalidId;

            if (result != SupplierError.None)
                throw new SupplierException(result);
            else
                return supplierDL.GetSupplier(id);
        }

        public bool DeleteSupplier(int id)
        {
            //Add validations here!
            SupplierError result = SupplierError.None;
            if (id <= 0)
                result |= SupplierError.InvalidId;

            if (result != SupplierError.None)
                throw new SupplierException(result);
            else
                return supplierDL.DeleteSupplier(id);
        }

    }
}
