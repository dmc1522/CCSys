using System;

namespace LasMargaritas.Models
{
    [Flags]
    public enum ProducerError
    {
        None = 0,
        ApiCommunicationError = 1,
        InvalidName = 2,
        InvalidGender = 4,
        InvalidCivilStatus = 8,
        InvalidId = 16
    }
}
