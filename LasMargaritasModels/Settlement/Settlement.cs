using System;
using System.Collections.ObjectModel;

namespace LasMargaritas.Models
{
    public class Settlement
    {
        public int Id { get; set; }
        public int ProducerId { get; set; }
        public int CicleId { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; } 
        public decimal WeightTicketsTotal { get; set; }
        public decimal CashAdvancesTotal { get; set; }        
        public decimal Total { get; set; }
        public bool Paid { get; set; }
        public decimal CreditsTotal { get; set; }
        public DateTime StoreTs { get; set; }
        public DateTime UpdateTs { get; set; }
        public Collection<int> WeightTicketsIds { get; set; }
        public string ProducerName { get; set; }
    }
}
