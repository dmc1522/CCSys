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
        public WareHouseException(WareHouseError error, string message):base(message)
        {
            Error = error;
        }
        public WareHouseException(string message) : base(message)
        {

        }
    }
}
