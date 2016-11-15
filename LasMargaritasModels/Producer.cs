using System;
using System.ComponentModel;
using System.Drawing;

namespace LasMargaritas.Models
{
    public class Producer:INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public string INE { get; set; }

        public string CURP { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public int StateId { get; set; }

        private byte[] _Photo;

        public event PropertyChangedEventHandler PropertyChanged;
     

        public byte[] Photo
        {
            get
            {
                return _Photo;
            }
            set
            {
                _Photo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Photo"));
            }
        }

        public string ZipCode { get; set; }

        public string RFC { get; set; }

        public int SexId { get; set; }

        public string PhoneNumber { get; set; }

        public string WorkPhoneNumber { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public int CivilStatusId { get; set; }

        public int? RegimeId { get; set; }

        public DateTime? StoreTS { get; set; }

        public DateTime? UpdateTS { get; set; }

        public string DistrinctOrColony { get; set; }

        public string BarCode { get; set; }

        public string Spouse { get; set; }

    }

}
