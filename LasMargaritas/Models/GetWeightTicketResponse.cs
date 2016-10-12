using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetWeightTicketResponse
    {
        public GetWeightTicketResponse()
        {
            WeightTickets = new List<WeightTicket>();
        }
        public string Message { get; set; }
        public bool Success { get; set; }
        public List<WeightTicket> WeightTickets { get; set; }
    }
}