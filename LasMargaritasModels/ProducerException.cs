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
    }
}
