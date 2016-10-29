using System;

namespace LasMargaritas.Models
{
    [Flags]
    public enum WareHouseError
    {
        None = 0,
        InvalidName = 1,
        InvalidAddress = 2,
        InvalidId = 4
    }
}
