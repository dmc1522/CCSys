using LasMargaritas.DL;
using LasMargaritas.Models;
using System.Collections.Generic;

namespace LasMargaritas.BL
{

    public class WeightTicketsBL
    {

        private WeightTicketsDL weightTicketsDL;

        public WeightTicketsBL(string connectionString)
        {
            weightTicketsDL = new WeightTicketsDL(connectionString);
        }

        public WeightTicket InsertWeightTicket(WeightTicket weightTicket)
        {
            //Add validations here!
            WeightTicketError result = WeightTicketError.None;
            if (weightTicket.CicleId <= 0)
                result |= WeightTicketError.InvalidCicle;
            if(weightTicket.ProducerId <= 0)
                result |= WeightTicketError.InvalidProducer;
            if (weightTicket.ProductId <= 0)
                result |= WeightTicketError.InvalidProduct;
            if (weightTicket.WarehouseId <= 0)
                result |= WeightTicketError.InvalidWareHouse;
            if (string.IsNullOrEmpty(weightTicket.Folio))
                result |= WeightTicketError.InvalidFolio;

            if (result != WeightTicketError.None)
                throw new WeightTicketException(result);
            else
                return weightTicketsDL.InsertWeightTicket(weightTicket);
        }

        public WeightTicket UpdateWeightTicket(WeightTicket weightTicket)
        {
            //Add validations here!
            WeightTicketError result = WeightTicketError.None;
            if (weightTicket.CicleId <= 0)
                result |= WeightTicketError.InvalidCicle;
            if (weightTicket.ProducerId <= 0)
                result |= WeightTicketError.InvalidProducer;
            if (weightTicket.ProductId <= 0)
                result |= WeightTicketError.InvalidProduct;
            if (string.IsNullOrEmpty(weightTicket.Folio))
                result |= WeightTicketError.InvalidFolio;
            if (weightTicket.WarehouseId <= 0)
                result |= WeightTicketError.InvalidWareHouse;

            if (result != WeightTicketError.None)
                throw new WeightTicketException(result);
            else
                return weightTicketsDL.UpdateWeightTicket(weightTicket);
        }

        public List<WeightTicket> GetWeightTicket(int? id = null)
        {
            //Add validations here!
            WeightTicketError result = WeightTicketError.None;
            if (id != null && id<=0)
                result |= WeightTicketError.InvalidId;

            if (result != WeightTicketError.None)
                throw new WeightTicketException(result);
            else
                return weightTicketsDL.GetWeightTicket(id);
        }

        public bool DeleteWeightTicket(int id)
        {
            //Add validations here!
            WeightTicketError result = WeightTicketError.None;
            if (id <= 0)
                result |= WeightTicketError.InvalidId;

            if (result != WeightTicketError.None)
                throw new WeightTicketException(result);
            else
                return weightTicketsDL.DeleteWeightTicket(id);
        }

    }
}
