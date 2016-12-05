using System;

namespace LasMargaritas.Models
{
    public class CicleException : Exception
    {
        public CicleError Error { get; set; }

        public CicleException(CicleError error)
        {
            Error = error;
        }
        public CicleException(CicleError error, string message):base(message)
        {
            Error = error;
        }
        public CicleException(string message) : base(message)
        {

        }
    }
}
