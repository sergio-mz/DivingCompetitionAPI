namespace DivingCompetitionAPI.Models
{
    public class Judge
    {
        public int JudgeId { get; set; }
        public string Name { get; set; } = "";

        public ICollection<CompetitionJudge> CompetitionJudges { get; set; } = new List<CompetitionJudge>();
    }
}
