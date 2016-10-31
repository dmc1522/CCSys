using LasMargaritas.DL;
using LasMargaritas.Models;
using System.Collections.Generic;


namespace LasMargaritas.BL
{
    public class ProducerBL
    {
        private ProducerDL producerDL;

        public ProducerBL(string connectionString)
        {
            producerDL = new ProducerDL(connectionString);
        }

        public Producer InsertProducer(Producer producer)
        {
            ProducerError result = ProducerError.None;
            if(string.IsNullOrEmpty(producer.Name.Trim()))
                result |= ProducerError.InvalidName;
            if (producer.SexId <= 0)
                result |= ProducerError.InvalidSex;
            if (producer.CivilStatusId <= 0)
                result |= ProducerError.InvalidCivilStatus;
            if (result != ProducerError.None)
                throw new ProducerException(result);
            else
                return producerDL.InsertProducer(producer);
        }

        public Producer UpdateProducer(Producer producer)
        {
            ProducerError result = ProducerError.None;
            if (string.IsNullOrEmpty(producer.Name))
                result |= ProducerError.InvalidName;
            if (producer.SexId <= 0)
                result |= ProducerError.InvalidSex;
            if (producer.CivilStatusId <= 0)
                result |= ProducerError.InvalidCivilStatus;
            if (result != ProducerError.None)
                throw new ProducerException(result);
            else
                return producerDL.UpdateProducer(producer);
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

        public bool DeleteProducer(int id)
        {
            //Add validations here!
            ProducerError result = ProducerError.None;
            if (id <= 0)
                result |= ProducerError.InvalidId;

            if (result != ProducerError.None)
                throw new ProducerException(result);
            else
                return producerDL.DeleteProducer(id);
        }

    }
}
