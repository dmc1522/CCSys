using System;

namespace LasMargaritas.Models
{
    public class RancherException : Exception
    {
        public RancherError Error { get; set; }

        public RancherException(RancherError error)
        {
            Error = error;
        }
        public RancherException(RancherError error, string message):base(message)
        {
            Error = error;
        }
        public RancherException(string message) : base(message)
        {
            
        }
    }
}
