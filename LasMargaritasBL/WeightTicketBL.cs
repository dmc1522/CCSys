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
            return weightTicketsDL.InsertWeightTicket(weightTicket);
        }

        public List<WeightTicket> GetWeightTicket()
        {
            //Add validations here!
            return weightTicketsDL.GetWeightTicket();
        }

    }
}
