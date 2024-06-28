namespace DivingCompetitionAPI.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public double Value { get; set; }
        public int JudgeId { get; set; }
        public Judge Judge { get; set; }
        public int DiveId { get; set; }
        public Dive Dive { get; set; }
    }
}
