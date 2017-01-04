using System;

namespace LasMargaritas.Models
{
    public class SettlementException:Exception
    {
        public SettlementError Error { get; set; }

        public SettlementException(SettlementError error)
        {
            Error = error;   
        }
        public SettlementException(SettlementError error, string message):base(message)
        {
            Error = error;
        }
        public SettlementException(string message) : base(message)
        {
         
        }
    }
}

