using System;

namespace LasMargaritas.Models
{
    public class WeightTicketException:Exception
    {
        public WeightTicketError Error { get; set; }

        public WeightTicketException(WeightTicketError error)
        {
            Error = error;   
        }
        public WeightTicketException(WeightTicketError error, string message):base(message)
        {
            Error = error;
        }
        public WeightTicketException(string message) : base(message)
        {
         
        }
    }
}

