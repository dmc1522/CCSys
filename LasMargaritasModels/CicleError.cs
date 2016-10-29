using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Models
{
    [Flags]
    public enum CicleError
    {
        None = 0,
        InvalidName = 1,
        InvalidStartDate = 2,
        InvalidDateZone1 = 4,
        InvalidDateZone2 = 8,
        InvalidAmount = 16,
        InvalidIsClosed = 32,
        InvalidId = 64
    }
}
