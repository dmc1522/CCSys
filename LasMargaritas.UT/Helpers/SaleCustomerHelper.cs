using LasMargaritas.Models;
using System;


namespace LasMargaritas.ULT
{
    public class SaleCustomerHelper
    {
        public static SaleCustomer  CreateDummySaleCustomer()
        {
            SaleCustomer saleCustomer = new SaleCustomer();
            saleCustomer.Address = "Direccion de prueba";
            saleCustomer.Name = "SaleCustomerTestName";
            return saleCustomer;
        }
    }

}
