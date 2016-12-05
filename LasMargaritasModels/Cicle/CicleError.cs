using System;

namespace LasMargaritas.Models
{
    [Flags]
    public enum CicleError
    {
        None = 0,
        ApiCommunicationError = 1,
        InvalidName = 2,
        InvalidStartDate = 4,
        InvalidDateZone1 = 8,
        InvalidDateZone2 = 16,
        InvalidAmount = 32,
        InvalidIsClosed = 64,
        InvalidId = 128
    }
}
