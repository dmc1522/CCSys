namespace LasMargaritas.Models
{
    public class RancherResponse
    {
        public RancherError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public Rancher Rancher { get; set; }
    }
}
