namespace LasMargaritas.Models
{
    public class SettlementResponse
    {
        public SettlementError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public Settlement Settlement { get; set; }
    }
}