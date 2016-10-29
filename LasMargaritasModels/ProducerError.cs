using System;

namespace LasMargaritas.Models
{
    [Flags]
    public enum ProducerError
    {
        None = 0,
        InvalidName = 1,
        InvalidSex = 2,
        InvalidCivilStatus = 4,
        InvalidId = 8
    }
}
