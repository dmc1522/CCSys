using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetSettlementResponse
    {
        public GetSettlementResponse()
        {
            Settlements = new List<Settlement>();
        }
        public SettlementError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<Settlement> Settlements { get; set; }
    }
}