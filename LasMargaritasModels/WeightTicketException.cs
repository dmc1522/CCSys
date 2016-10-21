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
    }
}
