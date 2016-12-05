using System;

namespace LasMargaritas.Models
{
    [Flags]
    public enum WareHouseError
    {
        None = 0,
        ApiCommunicationError = 1,
        InvalidName = 2,
        InvalidAddress = 4,
        InvalidId = 8
    }
}
