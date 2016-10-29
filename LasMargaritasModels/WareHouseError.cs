using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
