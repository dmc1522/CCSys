namespace LasMargaritas.Models
{
    public class CicleResponse
    {
        public CicleError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public Cicle Cicle { get; set; }
    }
}
