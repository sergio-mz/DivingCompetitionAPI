namespace DivingCompetitionAPI.Models
{
    public class Diver
    {
        public int DiverId { get; set; }
        public string Name { get; set; }
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        public ICollection<Dive> Dives { get; set; }
    }
}
