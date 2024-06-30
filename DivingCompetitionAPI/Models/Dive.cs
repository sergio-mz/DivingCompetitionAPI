namespace DivingCompetitionAPI.Models
{
    public class Dive
    {
        public int DiveId { get; set; }
        public string DiveCode { get; set; } = "";
        public double Difficulty { get; set; }

        public ICollection<DiverDive> DiverDives { get; set; } = new List<DiverDive>();
    }
}

