namespace DivingCompetitionAPI.Models
{
    public class DiverDive
    {
        public int DiverId { get; set; }
        public Diver? Diver { get; set; }

        public int DiveId { get; set; }
        public Dive? Dive { get; set; }

        public int CompetitionId { get; set; }
        public Competition? Competition { get; set; }

        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}

