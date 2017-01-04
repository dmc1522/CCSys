using System;
using System.ComponentModel;

namespace LasMargaritas.Models
{
    public class WeightTicket : INotifyPropertyChanged
    {
        public string ProductName { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaiseUpdateProperties()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public int Id { get; set; }
        public bool IsEntranceWeightTicket
        {
            get
            {
                return (EntranceWeightKg - ExitWeightKg) > 0;
            }
        }
        public string Number { get; set; }

        public float EntranceWeightKg { get; set; }

        public float ExitWeightKg { get; set; }

        public int CicleId { get; set; }

        public string UserId { get; set; }

        public float Humidity { get; set; }
        
        public float HumidityDiscount { get; set; }

        public float Impurities { get; set; }

        public float ImpuritiesDiscount { get; set; }

        public float TotalDiscount { get; set; }

        public float NetWeight { get; set; }

        public decimal Price { get; set; }

        public decimal SubTotal { get; set; }

        public string Plate { get; set; }

        public string Driver { get; set; }

        public bool Paid { get; set; }

        public DateTime StoreTs { get; set; }

        public DateTime UpdateTs { get; set; }

        public int ProductId { get; set; }

        public DateTime? EntranceDate { get; set; }

        public string EntranceWeigher { get; set; }

        public DateTime? ExitDate { get; set; }

        public string ExitWeigher { get; set; }

        public float EntranceNetWeight { get; set; }
                

        public float ExitNetWeight { get; set; }

        public float DryingDiscount { get; set; }
        public int? SupplierId { get; set; }
        public int? SaleCustomerId { get; set; }
        public int? RancherId { get; set; }
        public decimal TotalToPay { get; set; }

        public int WarehouseId { get; set; }

        public bool ApplyHumidity { get; set; }

        public bool ApplyImpurities { get; set; }

        public bool ApplyDrying { get; set; }
        
        public float SmallGrainDiscount { get; set; }
        public float TotalWeightToPay { get; set; }
        public bool Freight { get; set; }
        public bool FromFarmToPens { get; set; }
        public float DamagedGrainDiscount { get; set; }
        public int Cattle { get; set; }
        public float BrokenGrainDiscount { get; set; }

        public float CrashedGrainDiscount { get; set; }

        public string Folio { get; set; }

        public int ProducerId { get; set; }

    }
}
