namespace DivingCompetitionAPI.Models
{
    public class Dive
    {
        public int DiveId { get; set; }
        public string Code { get; set; }
        public double Difficulty { get; set; }
        public int DiverId { get; set; }
        public Diver Diver { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}
