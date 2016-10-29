using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
