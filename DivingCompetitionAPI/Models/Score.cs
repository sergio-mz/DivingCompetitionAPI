namespace DivingCompetitionAPI.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public double Points { get; set; }

        public int DiverDiveDiverId { get; set; }
        public int DiverDiveDiveId { get; set; }
        public int DiverDiveCompetitionId { get; set; }

        public DiverDive? DiverDive { get; set; }
        public int JudgeId { get; set; }
        public Judge? Judge { get; set; }
    }
}
