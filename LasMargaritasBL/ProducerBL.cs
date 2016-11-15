using LasMargaritas.DL;
using LasMargaritas.Models;
using System.Collections.Generic;


namespace LasMargaritas.BL
{
    public class ProducerBL
    {
        private ProducerDL producerDL;

        private LastModificationBL lastModificationBL;
        public ProducerBL(string connectionString)
        {
            producerDL = new ProducerDL(connectionString);
            lastModificationBL = new LastModificationBL(connectionString);
        }

        public Producer InsertProducer(Producer producer)
        {
            ProducerError result = ProducerError.None;
            if(string.IsNullOrEmpty(producer.Name.Trim()))
                result |= ProducerError.InvalidName;
            if (producer.GenderId <= 0)
                result |= ProducerError.InvalidGender;
            if (producer.CivilStatusId <= 0)
                result |= ProducerError.InvalidCivilStatus;
            if (result != ProducerError.None)
                throw new ProducerException(result);
            else
            {
                producer = producerDL.InsertProducer(producer);
                lastModificationBL.SetLastModification(new LastModification() { Module = Module.Producers });
                return producer;
            }
        }

        public Producer UpdateProducer(Producer producer)
        {
            ProducerError result = ProducerError.None;
            if (string.IsNullOrEmpty(producer.Name))
                result |= ProducerError.InvalidName;
            if (producer.GenderId <= 0)
                result |= ProducerError.InvalidGender;
            if (producer.CivilStatusId <= 0)
                result |= ProducerError.InvalidCivilStatus;
            if (result != ProducerError.None)
                throw new ProducerException(result);
            else
            {
                producer=  producerDL.UpdateProducer(producer);
                lastModificationBL.SetLastModification(new LastModification() { Module = Module.Producers });
                return producer;
            }
        }

        public List<Producer> GetProducer(int? id = null)
        {
            //Add validations here!
            ProducerError result = ProducerError.None;
            if (id != null && id <= 0)
                result |= ProducerError.InvalidId;

            if (result != ProducerError.None)
                throw new ProducerException(result);
            else
                return producerDL.GetProducer(id);
        }

        public List<SelectableModel> GetBasicModels()
        {
            return producerDL.GetBasicModels();
        }

        public bool DeleteProducer(int id)
        {
            //Add validations here!
            ProducerError result = ProducerError.None;
            if (id <= 0)
                result |= ProducerError.InvalidId;

            if (result != ProducerError.None)
                throw new ProducerException(result);
            else
            {
                bool success =  producerDL.DeleteProducer(id);
                if(success)
                    lastModificationBL.SetLastModification(new LastModification() { Module = Module.Producers });
                return success;
            }
        }

    }
}
