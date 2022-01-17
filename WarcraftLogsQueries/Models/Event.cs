using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    public class Event
    {
        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("sourceID")]
        public int SourceID { get; set; }

        [JsonPropertyName("targetID")]
        public int TargetID { get; set; }

        [JsonPropertyName("abilityGameID")]
        public int AbilityGameID { get; set; }

        [JsonPropertyName("fight")]
        public int Fight { get; set; }

        [JsonPropertyName("pin")]
        public string? Pin { get; set; }

        [JsonPropertyName("resourceChange")]
        public int ResourceChange { get; set; }

        [JsonPropertyName("resourceChangeType")]
        public int ResourceChangeType { get; set; }

        [JsonPropertyName("otherResourceChange")]
        public int OtherResourceChange { get; set; }

        [JsonPropertyName("maxResourceAmount")]
        public int MaxResourceAmount { get; set; }

        [JsonPropertyName("waste")]
        public int Waste { get; set; }

        [JsonPropertyName("reportData")]
        public ReportData? ReportData { get; set; }
    }
}
