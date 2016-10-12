namespace LasMargaritas.Models
{
    public class AddWeightTicketResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public WeightTicket WeightTicket { get; set; }
    }
}