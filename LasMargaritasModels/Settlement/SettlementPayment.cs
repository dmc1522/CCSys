using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Models
{
    public enum PaymentType
    {
        Cash,
        Check,
        WireTransfer
    }
    public class SettlementPayment
    {
        public string TypeString
        {
            get
            {
                return PaymentType.ToString();                
            }
        }
        public string BankAccount { get; set; }
        public int SettlementId { get; set; }
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public PaymentType PaymentType { get; set; }

        public string CheckOrVoucher { get; set; }

        public decimal Total { get; set; }

        public string User { get; set; }
    }
}
