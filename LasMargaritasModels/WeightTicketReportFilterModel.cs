using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Models
{
    public class WeightTicketReportFilterModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaiseUpdateProperties()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public int CicleId { get; set; }
        public int? ProducerId { get; set; }
        public int? SaleCustomerId { get; set; }
        public int? RancherId { get; set; }
        public int? SupplierId { get; set; }
        public int? ProductId { get; set; }
        public int? WeightTicketType { get; set; }
        public bool? EntranceWeightTicketsOnly { get; set; }
        public bool? ExitWeightTicketsOnly { get; set; }
        public string Folio { get; set; }
        public string Number { get; set; }
        public int? WareHouseId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }        

        public string GetUrlQuery()
        {           
            string query = "CicleId=" + CicleId.ToString();
            if (ProducerId.HasValue)
                query += "&ProducerId=" + ProducerId.ToString();
            if (SaleCustomerId.HasValue )
                query += "&SaleCustomerId=" + SaleCustomerId.ToString();
            if (RancherId.HasValue )
                query += "&RancherId=" + RancherId.ToString();
            if (SupplierId.HasValue)
                query += "&SupplierId=" + SupplierId.ToString();
            if (WeightTicketType.HasValue)
                query += "&WeightTicketType=" + ((int)WeightTicketType).ToString();
            if (EntranceWeightTicketsOnly.HasValue)
                query += "&EntranceWeightTicketsOnly=" + EntranceWeightTicketsOnly.ToString();
            if (ExitWeightTicketsOnly.HasValue)
                query += "&ExitWeightTicketsOnly=" + ExitWeightTicketsOnly.ToString();
            if (!string.IsNullOrEmpty(Folio))
                query += "&Folio=" + Folio;
            if (!string.IsNullOrEmpty(Number))
                query += "&Number=" + Number;
            if (WareHouseId.HasValue )
                query += "&WareHouseId=" + WareHouseId.ToString();
            if (StartDateTime.HasValue)
                query += "&StartDateTime=" + StartDateTime.Value.ToString("yyyy-MM-dd 00:00");
            if (EndDateTime.HasValue)
                query += "&EndDateTime=" + EndDateTime.Value.ToString("yyyy-MM-dd 23:59:59");
            return query;            
        }
    }
}
