using System.Text.Json.Serialization;

namespace DivingCompetitionAPI.Models
{
    public class Judge
    {
        public int JudgeId { get; set; }
        public string Name { get; set; } = "";


        [JsonIgnore]
        public ICollection<CompetitionJudge>? CompetitionJudges { get; set; }
        [JsonIgnore]
        public ICollection<Score>? Scores { get; set; }
    }
}
