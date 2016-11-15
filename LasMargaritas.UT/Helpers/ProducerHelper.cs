using LasMargaritas.Models;
using System;

namespace LasMargaritas.ULT
{
    public class ProducerHelper
    {
        public static Producer CreateDummyProducer()
        {
            Producer producer = new Producer();
            producer.Address = "testAddress";
            producer.BarCode = "testBarcode";
            producer.BirthDate = DateTime.Now;
            producer.City = "Ameca";
            producer.CivilStatusId = 1;
            producer.County = "Mexico";
            producer.CURP = "AA";
            producer.DistrinctOrColony = "ColoniaTest";
            producer.Email = "test@gmail.com";
            producer.Fax = "111223";
            producer.INE = "testINE";
            producer.LastName = "testLastName";
            producer.MobilePhoneNumber = "3313486214";
            producer.Name = "testName";
            producer.PhoneNumber = "38323825";
            producer.RegimeId = 1;
            producer.RFC = "Wawawa";
            producer.SexId = 1;
            producer.Spouse = "MargaritaEsposaTest";
            producer.StateId = 1;
            producer.StoreTS = DateTime.Now;
            producer.WorkPhoneNumber = "333";
            producer.ZipCode = "45138";
           
            return producer;
        }
    }
}
