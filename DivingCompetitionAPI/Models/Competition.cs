namespace DivingCompetitionAPI.Models
{
    public class Competition
    {
        public int CompetitionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Diver> Divers { get; set; }
        public ICollection<Judge> Judges { get; set; }
    }
}
