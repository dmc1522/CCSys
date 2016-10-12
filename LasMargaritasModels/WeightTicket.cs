namespace LasMargaritas.Models
{
    public class WeightTicket
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public double EntranceWeightKg { get; set; }
        public double ExitWeightKg { get; set; }
        public int CicleId { get; set; }
    }
}
