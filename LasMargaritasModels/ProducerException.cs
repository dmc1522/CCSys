using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
