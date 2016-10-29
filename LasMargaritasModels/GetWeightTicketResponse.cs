using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetWeightTicketResponse
    {
        public GetWeightTicketResponse()
        {
            WeightTickets = new List<WeightTicket>();
        }
        public WeightTicketError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<WeightTicket> WeightTickets { get; set; }
    }
}