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
    }
}
