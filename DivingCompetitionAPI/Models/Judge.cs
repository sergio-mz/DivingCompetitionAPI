namespace DivingCompetitionAPI.Models
{
    public class Judge
    {
        public int JudgeId { get; set; }
        public string Name { get; set; }
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
    }
}
