using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    public class Report
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("owner")]
        public Owner? Owner { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("startTime")]
        public long StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public long EndTime { get; set; }

        [JsonPropertyName("masterData")]
        public MasterData? MasterData { get; set; }

        [JsonPropertyName("fights")]
        public List<Fight>? Fights { get; set; }

        [JsonPropertyName("events")]
        public Events? Events { get; set; }
    }
}
