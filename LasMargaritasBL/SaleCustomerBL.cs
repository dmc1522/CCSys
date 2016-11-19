using LasMargaritas.DL;
using LasMargaritas.Models;
using System.Collections.Generic;


namespace LasMargaritas.BL
{
    public class SaleCustomerBL
    {
        private SaleCustomerDL saleCustomerDL;
        private const int WeightTicketSaleCustomerGroupId = 3;
        public SaleCustomerBL(string connectionString)
        {
            saleCustomerDL = new SaleCustomerDL(connectionString);
        }

        public SaleCustomer InsertSaleCustomer(SaleCustomer customer)
        {
            //Add validations here!
            SaleCustomerError result = SaleCustomerError.None;
            if (string.IsNullOrEmpty(customer.Name))
                result |= SaleCustomerError.InvalidName;
            if (customer.StateId <= 0)
                result |= SaleCustomerError.InvalidState;
            if (result != SaleCustomerError.None)
                throw new SaleCustomerException(result);
            else
                return saleCustomerDL.InsertSaleCustomer(customer);
        }

        public SaleCustomer UpdateSaleCustomer(SaleCustomer customer)
        {
            //Add validations here!
            SaleCustomerError result = SaleCustomerError.None;
            if (string.IsNullOrEmpty(customer.Name))
                result |= SaleCustomerError.InvalidName;
            if (customer.StateId <= 0)
                result |= SaleCustomerError.InvalidState;
            if (result != SaleCustomerError.None)
                throw new SaleCustomerException(result);
            else
                return saleCustomerDL.UpdateSaleCustomer(customer);
        }

        public List<SaleCustomer> GetSaleCustomer(int? id = null)
        {
            //Add validations here!
            SaleCustomerError result = SaleCustomerError.None;
            if (id != null && id <= 0)
                result |= SaleCustomerError.InvalidId;

            if (result != SaleCustomerError.None)
                throw new SaleCustomerException(result);
            else
                return saleCustomerDL.GetSaleCustomer(id);
        }

        public bool DeleteSaleCustomer(int id)
        {
            //Add validations here!
            SaleCustomerError result = SaleCustomerError.None;
            if (id <= 0)
                result |= SaleCustomerError.InvalidId;

            if (result != SaleCustomerError.None)
                throw new SaleCustomerException(result);
            else
                return saleCustomerDL.DeleteSaleCustomer(id);
        }

    }
}
