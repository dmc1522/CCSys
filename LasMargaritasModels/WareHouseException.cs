using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Models
{
    public class WareHouseException : Exception
    {
        public WareHouseError Error { get; set; }

        public WareHouseException(WareHouseError error)
        {
            Error = error;
        }
    }
}
