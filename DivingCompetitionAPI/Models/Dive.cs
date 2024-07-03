using System.Text.Json.Serialization;

namespace DivingCompetitionAPI.Models
{
    public class Dive
    {
        public int DiveId { get; set; }
        public string DiveCode { get; set; } = "";
        public char Group { get; set; } // 'A', 'B', 'C', 'D'
        public double Height { get; set; } // Altura en metros
        public double Difficulty { get; set; } // Dificultad suministrada

        [JsonIgnore]
        public ICollection<DiverDive>? DiverDives { get; set; }
    }
}

