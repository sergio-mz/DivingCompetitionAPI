using System.Text.Json.Serialization;

namespace DivingCompetitionAPI.Models
{
    public class Competition
    {
        public int CompetitionId { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public DateTime Date { get; set; }

        // Navegación hacia los CompetitionDiver y CompetitionJudge

        public ICollection<CompetitionDiver> CompetitionDivers { get; set; } = new List<CompetitionDiver>();
        
        public ICollection<CompetitionJudge> CompetitionJudges { get; set; } = new List<CompetitionJudge>();
        [JsonIgnore]
        public ICollection<DiverDive> DiverDives { get; set; } = new List<DiverDive>();
    }
}
