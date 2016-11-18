using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetRancherResponse
    {
        public GetRancherResponse()
        {
            Ranchers = new List<Rancher>();
        }
        public RancherError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<Rancher> Ranchers { get; set; }
    }
}