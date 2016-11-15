using System;

namespace LasMargaritas.Models
{
    public class ProducerException : Exception
    {
        public ProducerError Error { get; set; }

        public ProducerException(ProducerError error)
        {
            Error = error;
        }
        public ProducerException(ProducerError error, string message):base(message)
        {
            Error = error;
        }
        public ProducerException(string message) : base(message)
        {
            
        }
    }
}
