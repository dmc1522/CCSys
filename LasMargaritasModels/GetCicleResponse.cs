using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Models
{
    public class GetCicleResponse
    {
        public GetCicleResponse()
        {
            Cicles = new List<Cicle>();
        }
        public CicleError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<Cicle> Cicles { get; set; }
    }
}
