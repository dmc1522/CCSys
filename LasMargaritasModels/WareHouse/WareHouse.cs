using System;
using System.ComponentModel;
using System.Drawing;

namespace LasMargaritas.Models
{
    public class WareHouse : INotifyPropertyChanged
    {
        public void RaiseUpdateProperties()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

    }

}
