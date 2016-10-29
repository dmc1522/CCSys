using System;

namespace LasMargaritas.Models
{
    public class Cicle
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDateZone1 { get; set; }

        public DateTime EndDateZone2 { get; set; }

        public float AmountPerHectarea { get; set; }

        public bool Closed { get; set; }

    }

}
