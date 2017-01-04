using System;

namespace LasMargaritas.Models
{
    [Flags]
    public enum SettlementError
    {
        None = 0,
        InvalidCicle = 1,
        InvalidProducer = 2,
        InvalidId = 4,
        ApiCommunicationError = 8
    }
}
