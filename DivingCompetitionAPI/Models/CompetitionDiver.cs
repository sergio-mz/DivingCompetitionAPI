namespace DivingCompetitionAPI.Models
{
    public class CompetitionDiver
    {
        public int CompetitionId { get; set; }
        public Competition? Competition { get; set; }

        public int DiverId { get; set; }
        public Diver? Diver { get; set; }
    }
}
