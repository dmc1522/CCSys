using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetLastModificationResponse
    {
        public GetLastModificationResponse()
        {
            LastModifications = new List<LastModification>();
        }
        public LastModificationError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<LastModification> LastModifications { get; set; }
    }
}