using System.Text.Json.Serialization;

namespace DivingCompetitionAPI.Models
{
    public class Diver
    {
        public int DiverId { get; set; }
        public string Name { get; set; } = "";

        [JsonIgnore]
        public ICollection<CompetitionDiver> CompetitionDivers { get; set; } = new List<CompetitionDiver>();
        [JsonIgnore]
        public ICollection<DiverDive> DiverDives { get; set; } = new List<DiverDive>();
    }
}