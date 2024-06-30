namespace DivingCompetitionAPI.Models
{
    public class CompetitionJudge
    {
        public int CompetitionId { get; set; }
        public Competition? Competition { get; set; }

        public int JudgeId { get; set; }
        public Judge? Judge { get; set; }
    }
}
