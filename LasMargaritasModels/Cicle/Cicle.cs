using System;
using System.ComponentModel;
using System.Drawing;


namespace LasMargaritas.Models
{
    public class Cicle : INotifyPropertyChanged
    {
        public void RaiseUpdateProperties()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDateZone1 { get; set; }

        public DateTime EndDateZone2 { get; set; }

        public float AmountPerHectarea { get; set; }

        public bool Closed { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


    }

}
