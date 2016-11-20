using LasMargaritas.Models;
using System;


namespace LasMargaritas.ULT
{
    public class SupplierHelper
    {
        public static Supplier CreateDummySupplier()
        {
            Supplier supplier = new Supplier();
            supplier.Address = "TestAddress1";
            supplier.BankData = "Bank data test";
            supplier.CityOrDistrict = "AmecaTest";
            supplier.Contact = "testContact";
            supplier.MobilePhoneNumber = "333333152";
            supplier.Notes = "testNotes";            
            return supplier;
        }
    }

}
