using System;

namespace LasMargaritas.Models
{
    [Flags]
    public enum WeightTicketError
    {
        None = 0,
        InvalidCicle = 1,
        InvalidWareHouse = 2,
        InvalidFolio = 4,
        InvalidProducer = 8,
        InvalidProduct = 16,
        InvalidId = 32
    }
}
