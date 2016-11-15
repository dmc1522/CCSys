namespace LasMargaritas.Models
{
    public class ProducerResponse
    {
        public ProducerError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public Producer Producer { get; set; }
    }
}
