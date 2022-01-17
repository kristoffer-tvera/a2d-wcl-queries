using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    public class Fight
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("kill")]
        public bool Kill { get; set; }

        [JsonPropertyName("fightPercentage")]
        public double FightPercentage { get; set; }

        [JsonPropertyName("startTime")]
        public int StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public int EndTime { get; set; }
    }


}
