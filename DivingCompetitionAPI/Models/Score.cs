using System.Text.Json.Serialization;

namespace DivingCompetitionAPI.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int JudgeId { get; set; }
        public Judge? Judge { get; set; }

        public int DiverId { get; set; }
        public int DiveId { get; set; }
        public int CompetitionId { get; set; }

        [JsonIgnore]
        public DiverDive? DiverDive { get; set; }

        public double Points { get; set; }
    }
}
