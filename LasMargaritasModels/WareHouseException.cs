using System;

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
