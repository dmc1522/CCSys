using System;
using System.ComponentModel;
using System.Drawing;

namespace LasMargaritas.Models
{
    public class Supplier: INotifyPropertyChanged
    {
        public void RaiseUpdateProperties()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string CityOrDistrict { get; set; }

        public int StateId { get; set; }

        public string PhoneNumber { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string Contact { get; set; }

        public string BankData { get; set; }

        public string Notes { get; set; }

        public DateTime? StoreTs { get; set; }

        public DateTime? UpdateTs { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }

}
