namespace LasMargaritas.Models
{
    public class WeightTicketResponse
    {
        public WeightTicketError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public WeightTicket WeightTicket { get; set; }
    }
}