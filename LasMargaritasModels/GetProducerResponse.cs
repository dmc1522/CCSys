using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Models
{
    public class GetProducerResponse
    {
        public GetProducerResponse()
        {
            Producers = new List<Producer>();
        }
        public ProducerError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<Producer> Producers { get; set; }
    }
}