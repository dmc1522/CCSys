using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetSettlementPaymentResponse
    {
        public GetSettlementPaymentResponse()
        {
            Payments = new List<SettlementPayment>();
        }
        public SettlementError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<SettlementPayment> Payments { get; set; }

    }
}