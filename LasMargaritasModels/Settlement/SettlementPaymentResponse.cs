namespace LasMargaritas.Models
{
    public class SettlementPaymentResponse
    {
        public SettlementError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public SettlementPayment Payment { get; set; }
    }
}